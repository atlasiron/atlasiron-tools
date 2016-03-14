#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;
using System.Collections;
using System.Globalization;
using System.Xml;

using USP.Express.Pro.Exceptions;

namespace USP.Express.Pro
{
	/// <summary>
	/// Represents a parsed expression tree.
	/// </summary>
	public class ExpressionTree
	{
		internal ExpressionTree( ExpressionVariablesCollection cVariables, ArrayList aPolscaFormula )
		{
			m_cVariables = cVariables;
			m_aPolscaFormula = aPolscaFormula;
		}

		private static bool m_enmANSINulls = true;
		private static bool m_bNegativeOddRoot = false;

		// ==== Variables ====
		/// <summary>
		/// Determines if exceptions are to be thrown when an expression is being evaluated, or if "NaN" and "Infinity" values are to be used instead.
		/// </summary>
		/// <remarks>
		/// If <c>true</c>, exceptions are thrown. 
		/// If <c>false</c>, exceptions are not thrown and "NaN" and "Infinity" values are used.
		/// Default is <c>true</c>.
		/// </remarks>
		/// <example>		
		/// Example 1:		
		/// ThrowEvaluationExceptions: true
		/// Expression: 1/0 
		/// Result: <see cref="Exceptions.DivisionByZeroException"/> exception is thrown
		/// Example 2:		
		/// ThrowEvaluationExceptions: true
		/// Expression: 1/0 
		/// Result: Infinity
		/// </example>
		//public static bool ThrowEvaluationExceptions = true; // default is TRUE
				
		private ExpressionVariablesCollection m_cVariables;
		private VariableValuesCollection m_cVariableValues;

		// polish notation
		internal ArrayList m_aPolscaFormula;	

		// ==== Properties ====

		/// <summary>
		/// Gets the collection of variables. Read-only.
		/// </summary>				
		public ExpressionVariablesCollection Variables
		{
			get { return m_cVariables; } 
		}		
		
		/// <summary>
		/// Enables calculation of odd roots of negative real numbers.
		/// </summary>
		public static bool NegativeOddRoot
		{
			get { return m_bNegativeOddRoot; }
			set { m_bNegativeOddRoot = value; }
		}

		/// <summary>
		/// Returns the collection of variables used in the expression. Read-only.
		/// </summary>
		/// <returns>Collection of used variables</returns>
		/// <remarks>
		/// <para>For each variable in the collection, you can specify a corresponding value to be used in evaluation. 
		/// Then, you can pass this collection to the <see cref="Evaluate"/> method.
		/// </para>
		/// <para>
		/// This method is thread-safe. Each time it is called, a new copy of the collection is created.
		/// </para>
		/// </remarks>
		public VariableValuesCollection GetUsedVariables()
		{
			if ( m_cVariableValues == null )
			{
				ArrayList temp = new ArrayList();
				ArrayList indices = new ArrayList();
				for( int i = 0; i < m_aPolscaFormula.Count; ++i )
				{
					ItemInfo itemInfo = ( ItemInfo )m_aPolscaFormula[ i ];
					if ( itemInfo.type == ItemType.Variable )
					{
						if ( !indices.Contains( itemInfo.index ) )
						{
							temp.Add( new VariableValue( itemInfo.index, m_cVariables[ itemInfo.index ] ) );
							indices.Add( itemInfo.index );
						}
					}
				}
					
				m_cVariableValues = new VariableValuesCollection( temp );
			}

			return m_cVariableValues.Clone();
		}	
		
		// ==== Methods ==== 
		/// <overloads>
		/// <summary>Evaluates the expression.</summary>
		/// <remarks>This method is thread-safe</remarks>		
		/// </overloads>
		/// <summary>
		/// Evaluates the expression, given a collection of used variables with values, with case-insensitive string comparison enabled.
		/// </summary>
		/// <param name="values">Collection of actually used variables obtained with <see cref="GetUsedVariables()"/>, filled with the values.</param>
		/// <returns>The calculated value.</returns>		
		/// <exception cref="System.Exception"></exception>
		/// <exception cref="Exceptions.EvaluateException"></exception>
		public object Evaluate( VariableValuesCollection values )
		{ 		
			return Evaluate( values, false ); 
		}

		/// <summary>
		/// Evaluates the expression, given a collection of used variables with values.
		/// </summary>
		/// <param name="values">Collection of actually used variables obtained with <see cref="GetUsedVariables()"/>, filled with the values.</param>
		/// <param name="IsCaseSensitive">Determines if string comparisons are case-sensitive.</param>
		/// <returns>The calculated value.</returns>		
		/// <exception cref="System.Exception"></exception>
		/// <exception cref="Exceptions.EvaluateException"></exception>
		public object Evaluate( VariableValuesCollection values, bool IsCaseSensitive)
		{ 
			object[] aValues = new object[ Variables.Count ];

			foreach( VariableValue variableValue in values )
			{
				aValues[ variableValue.Index ] = variableValue.Value;
			}
			
			return Evaluate( aValues, IsCaseSensitive); 
		}

		/// <summary>
		/// Evaluates the expression with case-insensitive string comparison enabled.
		/// </summary>
		/// <param name="Values">Array of values of variables. The elements in this array must match variables in <see cref="ExpressionTree.Variables"/> collection. 
		/// The number of variables passed to <see cref="ExpressionTree.Evaluate"/> function must match the number of variables originally passed to <see cref="Parser.Parse"/> function.
		/// </param>
		/// <returns>The calculated value.</returns>		
		/// <exception cref="System.Exception"></exception>
		/// <exception cref="Exceptions.EvaluateException"></exception>
		public object Evaluate( object[] Values ) 
		{ 
			return Evaluate( Values, false ); 
		}

		/// <summary>
		/// Evaluates the expression.
		/// </summary>
		/// <param name="Values">Array of values of variables. The elements in this array must match variables in <see cref="ExpressionTree.Variables"/> collection.
		/// The number of variables passed to <see cref="ExpressionTree.Evaluate"/> function must match the number of variables originally passed to <see cref="Parser.Parse"/> function.
		/// </param>
		/// <param name="IsCaseSensitive">Determines if string comparisons are case-sensitive.</param>
		/// <returns>The calculated value.</returns>
		public object Evaluate( object[] Values, bool IsCaseSensitive )
		{
			// check Values count
			if ( Values.Length != m_cVariables.Count )
			{
				throw new InvalidParameterCountException();
			}

			ArrayList aValueStack = new ArrayList( 0 );

			ItemInfo item;			// item from Polish Notation

			object[] aParams;		// params to pass to a function or an object
			Type[] aParamTypes;		// param types
			bool bIsNullable = false; // used to indicate if function or operator returns DBNULL

			object result;

			int i, j;

			// evaluate expression
			for ( i=0; i < m_aPolscaFormula.Count; i++ )
			{
				item = (ItemInfo) m_aPolscaFormula[i];
				
				switch (item.type)
				{
					case ItemType.Operand:
						aValueStack.Add(item.itemValue);
						break;

					case ItemType.Constant:
						aValueStack.Add(item.GetConstant().Value);
						break;

                    case ItemType.VariableValue:
                        // check variable type?
                        if (item.index < 0)
                            aValueStack.Add(DBNull.Value);
                        else
                            aValueStack.Add(Values[item.index]);
                        break;

					case ItemType.Variable:
						// check variable type?
						aValueStack.Add(Values[item.index]);
						break;

					case ItemType.Function:
						
						Function oFunction = item.GetFunction();

						if (item.paramCount > 0)
						{
							aParamTypes = new Type[item.paramCount];
							for (j=item.paramCount-1; j>=0; j--)
							{
								aParamTypes[j] = (aValueStack[aValueStack.Count-1-(item.paramCount-1)+j]).GetType();
							}
							//??
							//if (!item.GetFunction().Validate(aParamTypes))
							//	throw new InvalidArgumentException();

							aParams = new object[item.paramCount];
							//aParams = Array.CreateInstance(typeof(object), item.paramCount);
							for (j=item.paramCount-1; j>=0; j--)
							{
								aParams[j] = aValueStack[aValueStack.Count-1];
								aValueStack.RemoveAt(aValueStack.Count-1);
							}

							// #2821 SVA.040722
							if (oFunction.UnfoldArray)
							{
								if (aParams.GetUpperBound(0) == 0 && aParams[0].GetType().IsArray) //array param
								{
									Array aParams1 = (Array) aParams[0];
									if ( aParams1.Rank == 1) //#3293 SVA.060228 
									{
										aParams = new object[aParams1.Length];
										Array.Copy(aParams1, aParams, aParams1.Length); // required for conversion between object[] and e.g. double[]
									}
								}							
							}
						
							// #2820
							bIsNullable = oFunction.IsNullable(aParams);

							if (bIsNullable) 
								result = System.DBNull.Value; 
							else
								result = oFunction.Evaluate(aParams, IsCaseSensitive);
								// Function.GetReturnType??
								// m_cFunctions.Item(item.index).GetReturnType(aParamTypes);
						}
						else
						{
							result = oFunction.Evaluate(new object[0], IsCaseSensitive);
							// Function.GetReturnType??
							// m_cFunctions.Item(item.index).GetReturnType();
						}

						aValueStack.Add(result);
						break;

					case ItemType.Operator:
						
						Operator oOperator = item.GetOperator();
						aParams = new object[oOperator.OperandsSupported];

						switch (oOperator.OperandsSupported)
						{
							case 1:
								//if (!item.GetOperator().Validate(aValueStack[aValueStack.Count-1].GetType()))
								//	throw new InvalidArgumentException();

									aParams[0] = aValueStack[aValueStack.Count-1];

									// Operator.GetReturnType??									
								break;
							case 2:
								//if (!item.GetOperator().Validate(aValueStack[aValueStack.Count-2].GetType(), aValueStack[aValueStack.Count-1].GetType()))
								//	throw new InvalidArgumentException();
								// check if aValueStack.Count >= 2 ??

								aParams[0] = aValueStack[aValueStack.Count-2];
								aParams[1] = aValueStack[aValueStack.Count-1];								

								//result = oOperator.Evaluate(new object[] {aValueStack[aValueStack.Count-2], aValueStack[aValueStack.Count-1]}, IsCaseSensitive);
								// Operator.GetReturnType??

								aValueStack.RemoveAt(aValueStack.Count - 1); 
								break;
						};

						// #2820
						bIsNullable = oOperator.IsNullable(aParams);

						if (bIsNullable) 
							result = System.DBNull.Value; 
						else
							result = oOperator.Evaluate(aParams, IsCaseSensitive);
						
						aValueStack[aValueStack.Count-1] = result;

						break;
				}
			}

			System.Diagnostics.Debug.Assert(aValueStack.Count == 1);
			result = aValueStack[aValueStack.Count-1];

			return result;
		}

		/// <summary>
		/// Returns the type which the expression returns. 
		/// </summary>
		/// <returns>The Type object that represents the type which the expression returns.</returns>
		/// <remarks>This method is thread-safe</remarks>
		public Type GetReturnType()
		{
			ArrayList aTypeStack = new ArrayList(0);
			Type[] aParamTypes;		// param types
			Type returnType;

			ItemInfo item;			// item from Polish Notation

			int i, j;

			// evaluate expression
			for (i=0; i < m_aPolscaFormula.Count; i++)
			{
				item = (ItemInfo) m_aPolscaFormula[i];
				
				switch (item.type)
				{
					case ItemType.Operand:
						aTypeStack.Add(item.itemValue.GetType());
						break;

					case ItemType.Constant:
						aTypeStack.Add(item.GetConstant().Value.GetType());
						break;

                    case ItemType.VariableValue:
                        if (item.index < 0)
                            aTypeStack.Add(typeof(DBNull));
                        else
                            aTypeStack.Add(m_cVariables[item.index].Type);
                        break;

                    case ItemType.Variable:
						aTypeStack.Add(m_cVariables[item.index].Type);
						break;

					case ItemType.Function:
						if (item.paramCount > 0)
						{
							aParamTypes = new Type[item.paramCount];
							for (j=item.paramCount-1; j>=0; j--)
							{
								aParamTypes[j] = (Type) aTypeStack[aTypeStack.Count-1];
								aTypeStack.RemoveAt(aTypeStack.Count-1);
							}

							returnType = item.GetFunction().GetReturnType(aParamTypes);
						}
						else
						{
							returnType = item.GetFunction().GetReturnType(new Type[] {});
						}

						aTypeStack.Add(returnType);
						break;

					case ItemType.Operator:
					switch (item.GetOperator().OperandsSupported)
					{
						case 1:
							returnType = item.GetOperator().GetReturnType(new Type[] {(Type) aTypeStack[aTypeStack.Count-1]});
							aTypeStack[aTypeStack.Count-1] = returnType;
							break;
						case 2:
							returnType = item.GetOperator().GetReturnType(new Type[] {(Type) aTypeStack[aTypeStack.Count-2], (Type) aTypeStack[aTypeStack.Count-1]});
							aTypeStack.RemoveAt(aTypeStack.Count - 1);
							aTypeStack[aTypeStack.Count-1] = returnType;
							break;
					};
						break;

				}
			}

			return ( Type )aTypeStack[ aTypeStack.Count - 1 ];
		}

		/// <summary>
		/// Checks if functions and operators in expression use valid arguments.
		/// </summary>
		/// <param name="ErrorPos">Position of invalid item in an expression.</param>
		/// <param name="InvalidArgumentIndex">Index of invalid function/operator argument.</param>
		/// <returns>true if all the arguments are valid, false otherwise</returns>
		internal bool Validate(ref int ErrorPos, ref int InvalidArgumentIndex)
		{
			ArrayList aTypeStack = new ArrayList(0);
			Type[] aParamTypes;		// param types
			Type returnType;
			int nInvalidArgument = -1;

			ItemInfo item;			// item from Polish Notation

			bool bIsValid = true;

			int i, j;

			// evaluate expression
			for (i=0; i < m_aPolscaFormula.Count; i++)
			{
				if (!bIsValid)
					break;

				item = (ItemInfo) m_aPolscaFormula[i];
				
				switch (item.type)
				{
					case ItemType.Operand:
						aTypeStack.Add(item.itemValue.GetType());
						break;

					case ItemType.Constant:
						aTypeStack.Add(item.GetConstant().Value.GetType());
						break;

					case ItemType.Variable:
						aTypeStack.Add(m_cVariables[item.index].Type);
						break;

                    case ItemType.VariableValue:
                        if (item.index < 0)
                            aTypeStack.Add(typeof(DBNull));
                        else
                            aTypeStack.Add(m_cVariables[item.index].Type);
                        break;

                    case ItemType.Function:
						if (item.paramCount > 0)
						{
							aParamTypes = new Type[item.paramCount];
							for (j=item.paramCount-1; j>=0; j--)
							{
								aParamTypes[j] = (Type) aTypeStack[aTypeStack.Count-1];
								aTypeStack.RemoveAt(aTypeStack.Count-1);
							}
							if (!item.GetFunction().Validate(aParamTypes, ref nInvalidArgument))
							{
								InvalidArgumentIndex = nInvalidArgument;
								ErrorPos = item.position;
								bIsValid = false;
								break;
							}
							returnType = item.GetFunction().GetReturnType(aParamTypes);
						}
						else
						{
							returnType = item.GetFunction().GetReturnType(new Type[] {});
						}

						aTypeStack.Add(returnType);
						break;

					case ItemType.Operator:
					switch (item.GetOperator().OperandsSupported)
					{
						case 1:
							if (!item.GetOperator().Validate(new Type[] {(Type) aTypeStack[aTypeStack.Count-1]}, ref nInvalidArgument))
							{
								InvalidArgumentIndex = nInvalidArgument;
								ErrorPos = item.position;
								bIsValid = false;
								break;
							}

							returnType = item.GetOperator().GetReturnType(new Type[] {(Type) aTypeStack[aTypeStack.Count-1]});
							aTypeStack[aTypeStack.Count-1] = returnType;
							break;
						case 2:
							if (!item.GetOperator().Validate(new Type[] {(Type) aTypeStack[aTypeStack.Count-2], (Type) aTypeStack[aTypeStack.Count-1]}, ref nInvalidArgument))
							{
								InvalidArgumentIndex = nInvalidArgument;
								ErrorPos = item.position;
								bIsValid = false;
								break;
							}

							returnType = item.GetOperator().GetReturnType(new Type[] {(Type) aTypeStack[aTypeStack.Count-2], (Type) aTypeStack[aTypeStack.Count-1]});
							aTypeStack.RemoveAt(aTypeStack.Count - 1);
							aTypeStack[aTypeStack.Count-1] = returnType;
							break;
					}
						break;

				}
			}

			return bIsValid;
		}

		/// <summary>
		/// Creates an XmlDocument object that represents an expression tree.
		/// </summary>
		/// <returns>XmlDocument object.</returns>
		public XmlDocument BuildXMLTree()
		{
			// build tree for parsed expression

			XmlDocument oTreeXML = new XmlDocument();
			XmlElement oRoot = oTreeXML.CreateElement("EXPRESSION");
			oTreeXML.AppendChild(oRoot);

			if (m_aPolscaFormula != null)
			{
				XmlElement oElement;

				ArrayList aNodeStack = new ArrayList(0);
				ItemInfo item;

				int i, j;
				for (i=0; i < m_aPolscaFormula.Count; i++)
				{
					item = (ItemInfo) m_aPolscaFormula[i];
				
					switch (item.type)
					{
						case ItemType.Operand:
						case ItemType.Constant:
						case ItemType.Variable:
							oElement = CreateXMLElement(ref oRoot, item);
							aNodeStack.Add(oElement);
							break;

						case ItemType.Function:
							oElement = CreateXMLElement(ref oRoot, item);
							if (item.paramCount > 0)
							{
								for (j=0; j<=item.paramCount-1; j++)
								{
									oElement.AppendChild((XmlNode) aNodeStack[aNodeStack.Count-item.paramCount+j]);
									aNodeStack.RemoveAt(aNodeStack.Count-item.paramCount+j);
								}
							}
							aNodeStack.Add(oElement);
							break;
						case ItemType.Operator:
							oElement = CreateXMLElement(ref oRoot, item);
						switch (item.GetOperator().OperandsSupported)
						{
							case 1:
								oElement.AppendChild ((XmlNode) aNodeStack[aNodeStack.Count-1]);
								aNodeStack[aNodeStack.Count-1] = oElement;
								break;
							case 2:
								oElement.AppendChild((XmlNode) aNodeStack[aNodeStack.Count-2]);
								oElement.AppendChild ((XmlNode) aNodeStack[aNodeStack.Count-1]);
								aNodeStack.RemoveAt(aNodeStack.Count - 1);
								aNodeStack[aNodeStack.Count-1] = oElement;
								break;
						};
							break;

					}
				}

				oRoot.AppendChild((XmlNode) aNodeStack[aNodeStack.Count-1]);
			}

			return oTreeXML;
		}

		/// <summary>
		/// Creates XmlElement object representing given token.
		/// </summary>
		/// <param name="oRoot">Root element of XmlDocument.</param>
		/// <param name="item">Token.</param>
		/// <returns>XmlElement object.</returns>
		private XmlElement CreateXMLElement(ref XmlElement oRoot, ItemInfo item)
		{
			string sCaption = "";
			string sType = "";
			string sDataType = "";
			switch (item.type)
			{
				case ItemType.Operand:
					sCaption = item.itemValue.ToString();
					sType = "Value";
					sDataType = item.itemValue.GetType().ToString();
					break;
				case ItemType.Constant:
					sCaption = item.GetConstant().Name;
					sType = "Constant";
					sDataType = item.GetConstant().Type.ToString();
					break;
				case ItemType.Variable:
					sCaption = m_cVariables[item.index].Name;
					sType = "Variable";
					sDataType = m_cVariables[item.index].Type.ToString();
					break;
				case ItemType.Function:
					sCaption = item.GetFunction().Name;
					sType = "Function";
					break;
				case ItemType.Operator:
					sCaption = item.GetOperator().Name;
					sType = "Operator";
					break;
			}

			XmlElement oElement = oRoot.OwnerDocument.CreateElement("NODE");
			oElement.SetAttribute("TYPE", sType);
			if (item.type == ItemType.Operand || item.type == ItemType.Constant || item.type == ItemType.Variable)
				oElement.SetAttribute("DATATYPE", sDataType);
			oElement.SetAttribute("VALUE", sCaption);
			return oElement;
		}

		/// <summary>
		/// Controls comparisons against <c>System.DBNull</c> values.
		/// </summary>
		/// <remarks>
		/// <para>
		///  With <see cref="ANSI_Nulls"/> set to <c>true</c>, the comparison operators EQUAL (=) and NOT EQUAL (&lt;&gt;) always return <c>DBNull</c> when one of its arguments is <c>DBNull</c>. 
		///  With <see cref="ANSI_Nulls"/> set to <c>false</c>, these operators return <c>TRUE</c> or <c>FALSE</c>, depending on whether both arguments are <c>DBNull</c>.
		///  </para>
		///  <para>
		///  Default is <c>true</c>.
		///  </para>
		/// </remarks>
		public static bool ANSI_Nulls
		{
			get { return m_enmANSINulls; } 		
			set {m_enmANSINulls = value; } 
		}

	}
}
