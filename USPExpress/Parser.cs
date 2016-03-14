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
using USP.Express.Pro.Constants;
using USP.Express.Pro.Functions;
using USP.Express.Pro.Operators;

namespace USP.Express.Pro
{
	/// <summary>
	/// Parses an expression string 
	/// </summary>
	public class Parser
	{
		/// <summary>
		/// Public constructor
		/// </summary>
		public Parser()
		{
			AddFunctionsCollection();
			AddOperatorsCollection();
			AddConstantsCollection();
			AddVariablesCollection();

			m_oNFI = new NumberFormatInfo();
			m_oNFI.NumberDecimalSeparator = ".";
		}

		private void AddVariablesCollection()
		{
			// Variable Collection
			m_cVariables = new VariablesCollection();
			m_cVariables.VariableAdd += new VariablesCollection.AddEventHandler(CheckName);
			m_cVariables.Changed += new VariablesCollection.ChangedEventHandler(DeleteVariablesCache);
		}

		private void AddConstantsCollection()
		{
			// Constant Collection
			m_cConstants = new ConstantsCollection();
			// add built-in constants
			m_cConstants.Add(new PiConstant());
			m_cConstants.Add(new EConstant());
			m_cConstants.Add(new TrueConstant());
			m_cConstants.Add(new FalseConstant());
			// DateInterval
			m_cConstants.Add(new YearConstant());
			m_cConstants.Add(new QuarterConstant());
			m_cConstants.Add(new MonthConstant());
			m_cConstants.Add(new DayOfYearConstant());
			m_cConstants.Add(new DayConstant());
			m_cConstants.Add(new WeekOfYearConstant());
			m_cConstants.Add(new WeekdayConstant());
			m_cConstants.Add(new HourConstant());
			m_cConstants.Add(new MinuteConstant());
			m_cConstants.Add(new SecondConstant());
			// FirstDayOfWeek
			m_cConstants.Add(new SystemConstant());
			m_cConstants.Add(new SundayConstant());
			m_cConstants.Add(new MondayConstant());
			m_cConstants.Add(new TuesdayConstant());
			m_cConstants.Add(new WednesdayConstant());
			m_cConstants.Add(new ThursdayConstant());
			m_cConstants.Add(new FridayConstant());
			m_cConstants.Add(new SaturdayConstant());
			// FirstWeekOfYear
			m_cConstants.Add(new Jan1Constant());
			m_cConstants.Add(new FirstFourDaysConstant());
			m_cConstants.Add(new FirstFullWeekConstant());
			// #2820
			//m_cConstants.Add(new DBNullConstant());

			m_cConstants.ConstantAdd += new ConstantsCollection.AddEventHandler(CheckName);
		}

		private void AddOperatorsCollection()
		{
			// Operators Collections
			ArrayList temp = new ArrayList();		
	
			// add built-in operators
			// !!! must be added in the order of OperatorType enum !!!
			temp.Add(new PlusModifier());
			temp.Add(new MinusModifier());
			temp.Add(new PlusOperator());
			temp.Add(new MinusOperator());
			temp.Add(new MultiplyOperator());
			temp.Add(new DivideOperator());
			temp.Add(new ModulusOperator());
			temp.Add(new PowerOperator());
			temp.Add(new IsLessThanOperator());
			temp.Add(new IsGreaterThanOperator());
			temp.Add(new IsEqualToOperator());
			temp.Add(new IsBasicEqualToOperator());
			temp.Add(new IsNotEqualToOperator());
			temp.Add(new IsBasicNotEqualToOperator());
			temp.Add(new IsLessThanOrEqualToOperator());
			temp.Add(new IsGreaterThanOrEqualToOperator());
			temp.Add(new AndOperator());
			temp.Add(new OrOperator());
			temp.Add(new NotOperator());
			temp.Add(new AndBasicOperator());
			temp.Add(new OrBasicOperator());
			temp.Add(new NotBasicOperator());
			temp.Add(new BitwiseAndOperator());
			temp.Add(new BitwiseInclusiveOrOperator());
			temp.Add(new BitwiseExclusiveOrOperator());
			temp.Add(new BitwiseComplement());
	
			m_cOperators = new OperatorsCollection( temp );
		}

		private void AddFunctionsCollection()
		{
			// functions collection
			m_cFunctions = new FunctionsCollection();
			
			// add built-in functions
			m_cFunctions.Add(new AbsFunction());
			m_cFunctions.Add(new ArcCosFunction());
			m_cFunctions.Add(new ArcCotFunction());
			m_cFunctions.Add(new ArcCscFunction());
			m_cFunctions.Add(new ArcSecFunction());
			m_cFunctions.Add(new ArcSinFunction());
			m_cFunctions.Add(new ArcTanFunction());
			m_cFunctions.Add(new CeilingFunction());
			m_cFunctions.Add(new CosFunction());
			m_cFunctions.Add(new HCosFunction());
			m_cFunctions.Add(new CotFunction());
			m_cFunctions.Add(new HCotFunction());
			m_cFunctions.Add(new CscFunction());
			m_cFunctions.Add(new HCscFunction());
			m_cFunctions.Add(new ExpFunction());
			m_cFunctions.Add(new FloorFunction());
			m_cFunctions.Add(new IntFunction());
			m_cFunctions.Add(new IIFFunction());
			m_cFunctions.Add(new IFFunction());
			m_cFunctions.Add(new AndFunction());
			m_cFunctions.Add(new OrFunction());
			m_cFunctions.Add(new LnFunction());
			m_cFunctions.Add(new LogFunction());
			m_cFunctions.Add(new Log10Function());
			m_cFunctions.Add(new NegFunction());
			m_cFunctions.Add(new PowerFunction());
			m_cFunctions.Add(new RootFunction());
			m_cFunctions.Add(new RandomFunction());
			m_cFunctions.Add(new SecFunction());
			m_cFunctions.Add(new HSecFunction());
			m_cFunctions.Add(new SinFunction());
			m_cFunctions.Add(new HSinFunction());
			m_cFunctions.Add(new SqrtFunction());
			m_cFunctions.Add(new SumFunction());
			m_cFunctions.Add(new TanFunction());
			m_cFunctions.Add(new HTanFunction());
			m_cFunctions.Add(new MinFunction());
			m_cFunctions.Add(new MaxFunction());
			m_cFunctions.Add(new RoundFunction());
			m_cFunctions.Add(new CountFunction());
			m_cFunctions.Add(new AverageFunction());
			// Date Functions
			m_cFunctions.Add(new NowFunction());
			m_cFunctions.Add(new TodayFunction());
			m_cFunctions.Add(new YearFunction());
			m_cFunctions.Add(new MonthFunction());
			m_cFunctions.Add(new DayFunction());
			m_cFunctions.Add(new WeekDayFunction());
			m_cFunctions.Add(new HourFunction());
			m_cFunctions.Add(new MinuteFunction());
			m_cFunctions.Add(new SecondFunction());
			m_cFunctions.Add(new DateAddFunction());
			m_cFunctions.Add(new DateDiffFunction());
			m_cFunctions.Add(new DatePartFunction());
			m_cFunctions.Add(new DateFunction());
			m_cFunctions.Add(new FormatFunction());
            m_cFunctions.Add(new StartOfWeekFunction());
            // String Functions
			m_cFunctions.Add(new LenFunction());
			m_cFunctions.Add(new LeftFunction());
			m_cFunctions.Add(new MidFunction());
			m_cFunctions.Add(new SubstituteFunction());
			m_cFunctions.Add(new LowerFunction());
			m_cFunctions.Add(new UpperFunction());
			m_cFunctions.Add(new TrimFunction());
			m_cFunctions.Add(new InFunction());
			m_cFunctions.Add(new FindFunction());
			m_cFunctions.Add(new IsDBNullFunction());
			m_cFunctions.Add(new IsNullFunction());
            m_cFunctions.Add(new IfNullFunction());
            m_cFunctions.Add(new NewGuidFunction());

			m_cFunctions.FunctionAdd += new FunctionsCollection.AddEventHandler( CheckName );
		}
		
		// ==== Constants ====

		internal const char DateIdentifier = '#';		// special delimiting characters
		internal const char StringIdentifier = '"';
        internal const char VariableValueIdentifier = '`';

		private const string sOperatorBeginLetters = "AaOoNn";	// first letters of operators
		
		// ==== Variables ====
		
		// functions, operators and constants
		private FunctionsCollection m_cFunctions;
		private ConstantsCollection m_cConstants;
		private OperatorsCollection m_cOperators;

		// variables
		private VariablesCollection m_cVariables;

		// expression variables
		private ExpressionVariablesCollection m_cVariablesCache;

		// NumberFormatInfo
		private NumberFormatInfo m_oNFI;


		// ==== Public Properties ====

		/// <summary>
		/// Returns the collection of variables. Read-only.
		/// </summary>
		public VariablesCollection Variables
		{
			get { return m_cVariables; } 
		}

		/// <summary>
		/// Returns the collection of functions. Read-only.
		/// </summary>
		public FunctionsCollection Functions
		{
			get { return m_cFunctions; } 
		}

		/// <summary>
		/// Returns the collection of constants. Read-only.
		/// </summary>
		public ConstantsCollection Constants
		{
			get { return m_cConstants; } 
		}

		/// <summary>
		/// Returns the collection of built-in operators. Read-only.
		/// </summary>
		public OperatorsCollection Operators
		{
			get { return m_cOperators; } 
		}
		
	
		// ==== Public Methods ==== 

		//<overloads>
		//<summary>Parses the expression.</summary>
		//<remarks>
		// <list type="number">
		// <item>
		// <description>
		// This method is thread-safe
		// </description> 
		// </item>
		// <item>
		// <description>
		// When implicit multiplication is enabled, you can omit the times symbol (*) in certain cases, as shown in the examples below:		
		//	<code>
		//	3X1 = 3*X1
		//	X1(2+1) = X1*(2+1)
		//	(X1+1)2 = (X1+1)*2
		//	(X1+3)(X1-3) = (X1+3)*(X1-3)
		//	</code> 
		//	Caution:
		//	Implicit multiplication has the same priority as regular multiplication. Thus, '1/3X1' is interpreted as '1/3*X1' not '1/(3*X1)'.
		//	</description> 
		// </item>		
		// </list>
		//</remarks>
		//</overloads>		
		/// <summary>
		/// Parses the expression.		
		/// </summary>
		/// <param name="Text">Expression to be parsed.</param>
		/// <returns>ExpressionTree object.</returns>				
		/// <exception cref="System.Exception"></exception>
		/// <exception cref="Exceptions.ParseException"></exception>
		/// <remarks>This method is thread-safe</remarks>
		public ExpressionTree Parse(string Text) { return Parse(Text, false); }
		
		/// <summary>
		/// Parses the expression with "Implicit Multiplication" option.
		/// </summary>
		/// <param name="Text">Expression to be parsed.</param>
		/// <param name="ImplicitMultiplication">Indicates whether implicit multiplication is supported.</param>		
		/// <returns>ExpressionTree object.</returns>
		/// <exception cref="System.Exception"></exception>
		/// <exception cref="Exceptions.ParseException"></exception>
		private ExpressionTree Parse(string Text, bool ImplicitMultiplication) // disable implicit multiplication due to SVA.040708 #2813
		{
			// check if expression is empty
			if (Text.Trim() == "") throw (new EmptyExpressionException());

			// variables
			int nPos = 0;		// position in expression string being parsed
			int nLastPos = 0;	// last function parameter position
			char symbol;		// current symbol

			string sItem = "";	// current item name
			int nIndex = 0;		// current item index
			ItemType nItemType = ItemType.Operand;			// current item type 
			ItemType m_lastItem = ItemType.OpeningParen;	// last item type for validating expression

			// operands
			double dNumber = 0;
			DateTime dtDate = new DateTime();

			int nParent = 0;			// for checking parentheses
			bool bDateInput = false;	// for date string input
            bool bStringInput = false;	// for string input
            bool bVariableValueInput = false;	// for string input

			FunctionInfo function;	// for checking param count
			ItemInfo item;

			// variables for getting polish notation
			ArrayList aPolscaFormula = new ArrayList(0);	// Polish Notation
			ArrayList aStack = new ArrayList(0);			// Stack for items
			ArrayList aFunctionStack = new ArrayList(0);	//array for storing information about functions in formula			

			// save parsed text
			string sText = Text.Trim();

			// cache variables
			lock (this) // tread-safety
			{
				if (m_cVariablesCache == null)
				{					
					RecreateVariablesCache();
				}
			}

			// begin
			do // while not end of Text
			{
				// get current symbol
				symbol = sText[ nPos ];

				// if processing date string
				if (bDateInput && symbol != DateIdentifier)
				{
					sItem += symbol;
				}
                // if processing variable value
                else if (bVariableValueInput && symbol != VariableValueIdentifier)
                {
                    sItem += symbol;
                }
                // if processing string
				else if (bStringInput && (symbol == StringIdentifier?sText[nPos-1] == '\\':true))
				{
					// check for escape characters
					switch (symbol)
					{
						case '\t': // tab
						case '\r': // carriage return
						case '\v': // vertical tab
						case '\f': // form feed
						case '\n': // new line
							throw new InvalidCharacterInStringException(nPos);
							break;
						default:
							sItem += symbol;
							break;
					}
				}
				// letter
				else if (IsLetter(symbol))
				{
					if (sItem == "")
					{
						// look for operators, only if char in Operator Begin Letters
						if (sOperatorBeginLetters.IndexOf(symbol)>-1 && FindOperator(sText, ref sItem, ref nPos, ref nIndex, m_lastItem))
						{
							// check if can add operator
							if (!CanAdd(ItemType.Operator, m_cOperators[nIndex].OperandsSupported, ref m_lastItem))
							{
								if (Functions.Search(sItem) == -1) // SVA.040708 #2813 if no function with the given name exists, raise an exception
									throw (new InvalidOperatorLocationException(nPos - sItem.Length + 1));
							}
							else
							{						

#if TRIAL
								switch (m_cOperators[nIndex].GetOperatorType())
								{
									case OperatorType.plusOperator:
									case OperatorType.multiplyOperator:
									case OperatorType.isGreaterThanOperator:
									case OperatorType.isEqualToOperator: //SVA.060810 enabled in trial version in order to illustrate operator overloading concept 
										break;
									default:
										throw (new OperatorNotSupportedInTrialVersionException(nPos - sItem.Length + 1));
								}
#endif

								AddToPolsca(ItemType.Operator, nIndex, nPos - sItem.Length + 1, ref aPolscaFormula, ref aStack);
								sItem = "";
							}
						}
						else 
							sItem += symbol;
					}
					else
					{
						if (IsNumber(sItem, out dNumber) && !IsScientificNotation(sText, nPos + 1))
						{
							// add number
							if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
								throw (new InvalidTermLocationException(nPos - sItem.Length));
										
							AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
							sItem = symbol.ToString();
						}
						else 
							sItem += symbol;
					}
				}
					// digit
				else if (Char.IsDigit(symbol))
				{
					sItem += symbol;
				}
				else	// special characters
				{
					switch (symbol)
					{
						case VariableValueIdentifier:
							if (bVariableValueInput) 
							{
                                // add variable value
								if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
									throw (new InvalidTermLocationException(nPos - sItem.Length - 1));

                                IsVariable(sItem, ref nIndex, ref nItemType);

                                AddToPolsca(ItemType.VariableValue, sItem, nPos - sItem.Length - 1, ref aPolscaFormula, nIndex);
								sItem = "";
								bVariableValueInput = false;
							}
							else
							{
                                bVariableValueInput = true;
							}
							break;

                        case DateIdentifier:
                            if (bDateInput)
                            {
                                if (IsDate(sItem, ref dtDate))
                                {
                                    // add date
                                    if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
                                        throw (new InvalidTermLocationException(nPos - sItem.Length - 1));

                                    AddToPolsca(ItemType.Operand, dtDate, nPos - sItem.Length - 1, ref aPolscaFormula);
                                    sItem = "";
                                    bDateInput = false;
                                }
                                else
                                    throw (new InvalidDateException(nPos - sItem.Length - 1));
                            }
                            else
                            {
                                bDateInput = true;
                            }
                            break;

                        case StringIdentifier:
							if (bStringInput) 
							{
								// add string
								if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
									throw (new InvalidTermLocationException(nPos - sItem.Length - 1));
										
								AddToPolsca(ItemType.Operand, sItem, nPos - sItem.Length - 1, ref aPolscaFormula);
								sItem = "";
								bStringInput = false;
							}
							else
							{
								bStringInput = true;
							}
							break;

						case '.':
							if (nPos > 0 && nPos < sText.Length-1 && Char.IsDigit(sText[nPos - 1]) && Char.IsDigit(sText[nPos + 1]))
								sItem += symbol;
							else
								throw (new UnexpectedSymbolException(nPos));
							break;

							//The following cases are all characters that typically are used as the first byte of operators
						case '-':
						case '+':
						case '*':
						case '/':
						case '%':
						case '^':
						case '<':
						case '>':
						case '=':
						case '!':
						case '~':
						case '&':
						case '|':
							if (sItem != "")
							{
								if (IsVariable(sItem, ref nIndex, ref nItemType))
								{
									// check if can add variable
									if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));

									AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
									sItem = "";
								}
								else if (IsNumber(sItem, out dNumber))
								{
									// add number
									if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));
										
									AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
									sItem = "";
								}
								else
								{
									if ((symbol == '+' || symbol == '-') && IsScientificNotation(sText, nPos)) 
									{
										sItem += symbol;
										break;
									}
									else 
										throw (new UnknownVariableException(nPos - sItem.Length));
								}
							}

							if (FindOperator(sText, ref sItem, ref nPos, ref nIndex, m_lastItem))
							{
								// check if can add operator
								if (!CanAdd(ItemType.Operator, m_cOperators[nIndex].OperandsSupported, ref m_lastItem))
									throw (new InvalidOperatorLocationException(nPos - sItem.Length + 1));

								#if TRIAL
								switch (m_cOperators[nIndex].GetOperatorType())
								{
									case OperatorType.plusOperator:
									case OperatorType.multiplyOperator:									
									case OperatorType.isGreaterThanOperator:
									case OperatorType.isEqualToOperator: //SVA.060810 enabled in trial version in order to illustrate operator overloading concept 
										break;
									default:
										throw (new OperatorNotSupportedInTrialVersionException(nPos - sItem.Length + 1));
								}
								#endif

								AddToPolsca(ItemType.Operator, nIndex, nPos - sItem.Length + 1, ref aPolscaFormula, ref aStack);
								sItem = "";
							}
							break;

						case ' ':
                        // escape characters
						case '\t': // tab
						case '\r': // carriage return
						case '\v': // vertical tab
						case '\f': // form feed
						case '\n': // new line 
							//
							if (sItem != "")
							{
								if (IsVariable(sItem, ref nIndex, ref nItemType))
								{
									// check if can add variable
									if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));

									AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
									sItem = "";
								}
								else if (IsNumber(sItem, out dNumber))
								{
									// add number
									if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));
										
									AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
									sItem = "";
								}
								else
								{
									// error: Not Variable and Not Number
									throw (new UnknownVariableException(nPos - sItem.Length));
								}
							}
							break;

						case '(':
							nParent++;	// increase opening parenthesis count

							if (sItem != "")
							{
								if (IsFunction(sItem, ref nIndex, ref nItemType))
								{
									// check if can add function
									if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidFunctionLocationException(nPos - sItem.Length));

#if TRIAL
									if (m_cFunctions[nIndex].GetType().Equals(typeof(TodayFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(YearFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(MonthFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(DayFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(HourFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(MinuteFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(SecondFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(WeekDayFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(DateAddFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(DateDiffFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(DatePartFunction)) ||
										m_cFunctions[nIndex].GetType().Equals(typeof(DateFunction))
										)
									{
										throw (new FunctionNotSupportedInTrialVersionException(nPos - sItem.Length));
									}
#endif

									AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
									sItem = "";
										
									// begin count parameters for this function
									function.parentCount = nParent - 1;
									function.stackIndex = aStack.Count - 1;
									function.paramCount = 1;
									aFunctionStack.Add (function);
								}
								else
								{
									if (ImplicitMultiplication)	// Implicit Multiplication
									{
										if (IsVariable(sItem, ref nIndex, ref nItemType))	// expression like "x1(...)" or "PI(...)"
										{
											// check if can add variable
											if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
												throw (new InvalidTermLocationException(nPos - sItem.Length));

											AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
											sItem = "";
										}
										else if (IsNumber(sItem, out dNumber))	// expression like "3(...)"
										{
											// add number
											if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
												throw (new InvalidTermLocationException(nPos - sItem.Length));
										
											AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
											sItem = "";
										}
										else
										{
											// error: Not Function
											throw (new UnknownFunctionException(nPos - sItem.Length));
										}
									}
									else
									{
										// error: Not Function
										throw (new UnknownFunctionException(nPos - sItem.Length));
									}
								}
							}

							// check if can add '('
							if (!CanAdd(ItemType.OpeningParen, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
								throw (new InvalidParenthesisLocationException(nPos - sItem.Length));

							AddToPolsca(ItemType.OpeningParen, ref aPolscaFormula, ref aStack);

							nLastPos = nPos+1;
							break;
						case ',':
							if (sItem != "")
							{
								if (IsVariable(sItem, ref nIndex, ref nItemType))
								{
									// check if can add variable
									if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));

									AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
									sItem = "";
								}
								else if (IsNumber(sItem, out dNumber))
								{
									// add number
									if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));
										
									AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
									sItem = "";
								}
								else // error: Not Variable and Not Number
									throw (new UnknownVariableException(nPos-sItem.Length));
							}
							
							if (aFunctionStack.Count > 0)
							{
								// increase paramCount for current function
								function = (FunctionInfo) aFunctionStack[aFunctionStack.Count - 1];
								function.paramCount++;
								aFunctionStack[aFunctionStack.Count - 1] = function;
							}
							else
							{
								// error: ',' without function
								throw (new UnexpectedSymbolException(nPos));
							}

							// check if can add ','
							if (!CanAdd(ItemType.Comma, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
								throw (new InvalidCommaLocationException(nPos));

							AddToPolsca(ItemType.Comma, ref aPolscaFormula, ref aStack);
							break;

						case ')':
							if (sItem != "")
							{
								if (IsVariable(sItem, ref nIndex, ref nItemType))
								{
									// check if can add variable
									if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));
										
									AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
									sItem = "";
								}
								else if (IsNumber(sItem, out dNumber))
								{
									// add number
									if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
										throw (new InvalidTermLocationException(nPos - sItem.Length));
										
									AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
									sItem = "";
								}
								else
								{
									// error: Not Variable and Not Number
									throw (new UnknownVariableException(nPos-sItem.Length));
								}
							}

							if (nParent == 0)
								throw (new UnbalancedParenthesesException());

							nParent--;
								
							if (aFunctionStack.Count > 0)
							{
								function = (FunctionInfo) aFunctionStack[aFunctionStack.Count-1];
								if (function.parentCount == nParent)
								{
									// set paramCount for current function
									item = (ItemInfo) aStack[function.stackIndex];
										
									if (sText[nPos-1] == '(')
										item.paramCount = 0;	//no params
									else
										item.paramCount = function.paramCount;

									// check if function supports this paramCount 
									switch (item.type)
									{
										case ItemType.Function:
											if (!item.GetFunction().MultArgsSupported(item.paramCount))
												//												throw (new WrongParametersNumberException(nPos));
												throw (new WrongArgumentsNumberException(nLastPos));
											break;
									}

									aStack[function.stackIndex] = item;
									aFunctionStack.RemoveAt(aFunctionStack.Count - 1);
										
									// if paramCount = 0, add ")" without checking
									if (item.paramCount == 0)
									{
										m_lastItem = ItemType.ClosingParen;
										AddToPolsca(ItemType.ClosingParen, ref aPolscaFormula, ref aStack);
										break;
									}
								}
							}

							// check if can add ')'
							if (!CanAdd(ItemType.ClosingParen, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
								throw (new InvalidParenthesisLocationException(nPos - sItem.Length));

							AddToPolsca(ItemType.ClosingParen, ref aPolscaFormula, ref aStack);
							break;

						case '?':
						case ':':
						default:
							throw (new UnexpectedSymbolException(nPos));

					};
				}
				nPos ++;
			}
			while (nPos < sText.Length);
			// end

			// check if date input is finished
			if (bDateInput)
				throw (new MissingSymbolException(DateIdentifier));

			// check if string input is finished
			if (bStringInput)
				throw (new MissingSymbolException(StringIdentifier));

			if (sItem != "")
			{
				if (IsVariable(sItem, ref nIndex, ref nItemType))
				{
					// check if can add variable
					if (!CanAdd(nItemType, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
						throw (new InvalidTermLocationException(nPos - sItem.Length));

					AddToPolsca(nItemType, nIndex, nPos - sItem.Length, ref aPolscaFormula, ref aStack);
					sItem = "";
				}
				else if (IsNumber(sItem, out dNumber))
				{
					// add number
					if (!CanAdd(ItemType.Operand, ref m_lastItem, nPos - sItem.Length, ImplicitMultiplication, ref aPolscaFormula, ref aStack))
						throw (new InvalidTermLocationException(nPos - sItem.Length));
										
					AddToPolsca(ItemType.Operand, dNumber, nPos - sItem.Length, ref aPolscaFormula);
					sItem = "";
				}
				else
				{
					// error: Not Variable and Not Number
					throw (new UnknownVariableException(nPos-sItem.Length));
				}
			}

			// check parentheses
			if (nParent > 0)
				throw (new UnbalancedParenthesesException());

			// check m_lastItem
			if (m_lastItem != ItemType.Variable && m_lastItem != ItemType.ClosingParen && m_lastItem != ItemType.Constant && m_lastItem != ItemType.Operand)
				throw (new InvalidExpressionException(nPos - sItem.Length));

			//get items from Polish Notation Stack
			if (aStack.Count > 0)
				do 
				{
					item = (ItemInfo) aStack[aStack.Count-1];
					aPolscaFormula.Add (item);
					aStack.Remove (item);
				}
				while (aStack.Count > 0);
		
			// create ExpressionTree object
			ExpressionTree oTree = new ExpressionTree( m_cVariablesCache, aPolscaFormula );			

			int nErrPos = -1;
			int nErrIndex = -1;
			if (!oTree.Validate(ref nErrPos, ref nErrIndex))
				throw new InvalidArgumentTypeException(nErrPos, nErrIndex + 1);

#if TIME_TRIAL
			if (DateTime.Now > new DateTime(2006, 7, 7))
				throw (new TrialVersionExpiredException());
#endif		

			return oTree;
		}

		// ==== Private Functions ==== 

		/// <summary>
		/// Creates a "copy snapshot" of Parser.Variables collection. 
		/// Stores it in ExpressionVariables collection.		
		/// </summary>
		/// <remarks>
		/// A reference to ExpressionVariables collection is then passed to the ExpressionTree object.
		/// </remarks> 
		private void RecreateVariablesCache()
		{
			ArrayList temp = new ArrayList( m_cVariables.Count );			
			for ( int i = 0; i < m_cVariables.Count; ++i )
			{
				temp.Add( m_cVariables[ i ].Clone() );
			}

			m_cVariablesCache = new ExpressionVariablesCollection( temp );
		}

		/// <summary>
		/// Deletes current ExpressionVariables collection.
		/// </summary>
		private void DeleteVariablesCache()
		{
			m_cVariablesCache = null;
		}

		/// <summary>
		/// Returns a value indicating whether the specified symbol can be used in a variable, constant or function names
		/// </summary>
		/// <param name="cSymbol">A Unicode character.</param>
		/// <returns>true, if symbol can be used in variable, constant or function names; otherwise, false.</returns>
		private bool IsIdentifierCharacter(char cSymbol)
		{
			return (IsLetter(cSymbol) || Char.IsDigit(cSymbol));
		}

		/// <summary>
		/// Returns a value indicating whether the specified symbol is categorized as an alphabetic letter or '_'.
		/// </summary>
		/// <param name="cSymbol">A Unicode character.</param>
		/// <returns>true, if symbol  is categorized as an alphabetic letter or '_'; otherwise, false.</returns>
		private bool IsLetter(char cSymbol)
		{
            return (Char.IsLetter(cSymbol) || cSymbol == '_' || cSymbol == '{' || cSymbol == '}');
		}

		/// <summary>
		/// Returns a value indicating whether the specified item name is found in the Variables collection or Constants collection.
		/// </summary>
		/// <param name="sItem">Item name.</param>
		/// <param name="nIndex">Index in Variables collection or Constants collection.</param>
		/// <param name="nItemType">ItemType (Variable or Constant).</param>
		/// <returns>True, if item name is found in Variables collection or Constants collection; otherwise, false.</returns>
		private bool IsVariable(string sItem, ref int nIndex, ref ItemType nItemType)
		{
			// returns true if finds Variable or Constant
			nIndex = m_cVariables.Search(sItem);
			if (nIndex > -1)
			{
				nItemType = ItemType.Variable;
				return true;
			}
			nIndex = m_cConstants.Search(sItem);
			if (nIndex > -1)
			{
				nItemType = ItemType.Constant;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns a value indicating whether the specified string can be converted to Double type.
		/// </summary>
		/// <param name="sItem">A string containing a number to convert.</param>
		/// <param name="dNumber">Converted number.</param>
		/// <returns>True, if a specified string can be converted to Double type; otherwise false.</returns>
		private bool IsNumber(string sItem, out double dNumber)
		{
			bool bIsNumber = Double.TryParse(sItem, NumberStyles.Float, m_oNFI, out dNumber);			

			//SVA.041228 - exceptions are slow, so do not use Convert.ToDouble 			
			//dNumber = Convert.ToDouble(sItem, m_oNFI);

			return bIsNumber;
		}

		/// <summary>
		/// Returns a value indicating whether the specified string can be converted to DateTime type.
		/// </summary>
		/// <param name="sItem">A string containing a number to convert.</param>
		/// <param name="dtDate">Converted date.</param>
		/// <returns>True, if a specified string can be converted to DateTime type; otherwise false.</returns>
		private bool IsDate(string sItem, ref DateTime dtDate)
		{
			bool bIsDate = true;
			try 
			{
				dtDate = System.Convert.ToDateTime(sItem);
			}
			catch 
			{
				bIsDate = false;
			}

			return bIsDate;
		}

		/// <summary>
		/// Returns a value indicating whether the specified item name is found in the <see cref="Functions"/> collection.
		/// </summary>
		/// <param name="sItem">Item name.</param>
		/// <param name="nIndex">Index in <see cref="Functions"/> collection.</param>
		/// <param name="nItemType">ItemType (Function).</param>
		/// <returns>True, if item name is found in Functions collection; otherwise, false.</returns>
		private bool IsFunction(string sItem, ref int nIndex, ref ItemType nItemType)
		{
			//returns true if finds Function or User Function
			nIndex = m_cFunctions.Search(sItem);
			if (nIndex > -1)
			{
				nItemType = ItemType.Function;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Checks if there is an operator at current position in an expression string.
		/// If found, advances to the next token position.
		/// </summary>
		/// <param name="sText">Expression string.</param>
		/// <param name="sItem">Name of the operator, if found.</param>
		/// <param name="nPos">Current position in an expression string.</param>
		/// <param name="nIndex">Index in Operators collection.</param>
		/// <param name="lastItem">Type of the preceding token.</param>
		/// <returns>True, if item name is found in Operators collection; otherwise, false.</returns>
		private bool FindOperator(string sText, ref string sItem, ref int nPos, ref int nIndex, ItemType lastItem)
		{
			// returns true if operator found
			OperatorType nOperatorType = OperatorType.noOperator;

			switch( sText[nPos] )
			{	
				case '+':
					if (IsSignModifier(nPos, lastItem))
						nOperatorType = OperatorType.plusModifier;
					else
						nOperatorType = OperatorType.plusOperator;
					break;
				case '-':
					if (IsSignModifier(nPos, lastItem))
						nOperatorType = OperatorType.minusModifier;
					else
						nOperatorType = OperatorType.minusOperator;
					break;
				case '*':
					nOperatorType = OperatorType.multiplyOperator;
					break;
				case '/':
					nOperatorType = OperatorType.divideOperator;
					break;
				case '%':
					nOperatorType = OperatorType.modulusOperator;
					break;
				case '^':
					nOperatorType = OperatorType.powerOperator;
					break;
				case '<':
					nOperatorType = OperatorType.isLessThanOperator;
					if(nPos < sText.Length-1)
					{
						switch (sText[nPos+1])
						{
							case '=':
								nOperatorType = OperatorType.isLessThanOrEqualToOperator;
								break;
							case '>':
								nOperatorType = OperatorType.isBasicNotEqualToOperator;
								break;
						}
					}
					break;
				case '>':
					nOperatorType = OperatorType.isGreaterThanOperator;
					if(nPos < sText.Length-1)
					{
						switch (sText[nPos+1])
						{
							case '=':
								nOperatorType = OperatorType.isGreaterThanOrEqualToOperator;
								break;
						}
					}
					break;
				case '!':
					nOperatorType = OperatorType.notOperator;
					if(nPos < sText.Length-1)
					{
						switch (sText[nPos+1])
						{
							case '=':
								nOperatorType = OperatorType.isNotEqualToOperator;
								break;
							case '&':
								nOperatorType = OperatorType.bitwiseExclusiveOrOperator;
								break;
						}
					}
					break;
				case '=':
					nOperatorType = OperatorType.isBasicEqualToOperator;
					if(nPos < sText.Length-1)
					{
						switch (sText[nPos+1])
						{
							case '=':
								nOperatorType = OperatorType.isEqualToOperator;
								break;
						}
					}
					break;
				case '&':
					nOperatorType = OperatorType.bitwiseAndOperator;
					if(nPos < sText.Length-1)
					{
						switch (sText[nPos+1])
						{
							case '&':
								nOperatorType = OperatorType.andOperator;
								break;
						}
					}
					break;
				case '|':
					nOperatorType = OperatorType.bitwiseInclusiveOrOperator;
					if(nPos < sText.Length-1)
					{
						switch (sText[nPos+1])
						{
							case '|':
								nOperatorType = OperatorType.orOperator;
								break;
						}
					}
					break;
				case '~':
					nOperatorType = OperatorType.bitwiseComplement;
					break;
				case 'a':
				case 'A':
					// AND
					// Bug #2521
					if ((nPos <= sText.Length - 3) && ((nPos < sText.Length - 3)?(!IsIdentifierCharacter(sText[nPos+3])):true) && (String.Compare(sText.Substring(nPos,3),"and",true) == 0)) // "AND"
					{
						nOperatorType = OperatorType.andBasicOperator;
					}
					break;
				case 'o':
				case 'O':
					// OR
					// Bug #2521
					if ((nPos <= sText.Length - 2) && ((nPos < sText.Length - 2)?(!IsIdentifierCharacter(sText[nPos+2])):true) && (String.Compare(sText.Substring(nPos,2),"or",true) == 0)) // "OR"
					{
						nOperatorType = OperatorType.orBasicOperator;
					}
					break;
				case 'n':
				case 'N':
					// NOT
					// Bug #2521
					if ((nPos <= sText.Length - 3) && ((nPos < sText.Length - 3)?(!IsIdentifierCharacter(sText[nPos+3])):true) && (String.Compare(sText.Substring(nPos,3),"not",true) == 0)) // "NOT"
					{
						nOperatorType = OperatorType.notBasicOperator;
					}
					break;
			}

			if (nOperatorType != OperatorType.noOperator) 
			{
				sItem = m_cOperators[(int) nOperatorType].Name; 
				nPos += sItem.Length - 1;
			}

			nIndex = (int) nOperatorType;

			if (nOperatorType == OperatorType.noOperator)
				return false;
			else
				return true;
		}

		/// <summary>
		/// Returns a value indicating whether '+' or '-' is used as unary sign Modifier.
		/// </summary>
		/// <param name="nPos">Current position in an expression string.</param>
		/// <param name="lastItem">Type of the last parsed item.</param>
		/// <returns>True, if '+' or '-' is used as sign Modifier; otherwise, false.</returns>
		private bool IsSignModifier(int nPos, ItemType lastItem)
		{
			if (lastItem == ItemType.OpeningParen || lastItem == ItemType.Comma || lastItem == ItemType.Operator)
				return true;

			return false;
		}

		/// <summary>
		/// Returns a value indicating if occurrence of '+' or '-' character belongs to a number in scientific notation.
		/// </summary>
		/// <param name="sText">Expression string.</param>
		/// <param name="nPos">Current position in an Expression string.</param>
		/// <returns>True, if scientific notation is used; otherwise, false.</returns>
		private bool IsScientificNotation(string sText, int nPos)
		{
			if ((nPos>=2 && nPos<sText.Length-1)?(sText[nPos-1] == 'e' || sText[nPos-1] == 'E') && (Char.IsDigit(sText[nPos]) || sText[nPos] == '-' || sText[nPos] == '+') && Char.IsDigit(sText[nPos-2]):false)
				return true;

			return false;
		}

		/// <summary>
		/// Checks if the specified identifier is valid and is unique among defined Variables, Constants, Functions and Operators.
		/// </summary>
		/// <param name="sNewName">New identifier.</param>
		/// <remarks>
		/// Is called when adding new variables, aliases, functions or constants.
		/// </remarks>
		private void CheckName(string sNewName)
		{
			// check if new name is valid
            if (sNewName == null || !System.Text.RegularExpressions.Regex.IsMatch(sNewName, "^[\\{\\p{L}\\p{Pc}]\\w*[\\}]*$"))
				throw new InvalidIdentifierException();
			
			// check if new name is unique
			bool bIsUnique = true;

			if (m_cVariables.Search(sNewName) > -1)	// search variables collection
				bIsUnique = false;
			else if (m_cFunctions.Search(sNewName) > -1)	// search functions collection
				bIsUnique = false;
			else if (m_cConstants.Search(sNewName) > -1)	// search constants collection
				bIsUnique = false;
			else	// search operators collection
			{
				int i;
				for (i=0; i<m_cOperators.Count; i++)
				{
					if (String.Compare(sNewName, m_cOperators[i].Name, true) == 0)
					{
						bIsUnique = false;
						break;
					}
				}
			}

			if (!bIsUnique)
				throw new DuplicateIdentifierException();
		}

		// --- add items to Polish Notation ---

		/// <summary>
		/// Add item to Polish Notation array.
		/// </summary>
		/// <param name="nItemType">Item type.</param>
		/// <param name="oValue">Item value.</param>
		/// <param name="nPosition">Position in an expression string being parsed.</param>
		/// <param name="aPolscaFormula">Polish Notation array.</param>
		/// <remarks>Overloaded version, is used to add numbers, dates and strings.</remarks>
		private void AddToPolsca(ItemType nItemType, object oValue, int nPosition, ref ArrayList aPolscaFormula, int nIndex = -1)
		{
			// adds item to polish notation
			ItemInfo item = new ItemInfo();

			switch (nItemType)
			{
				case ItemType.Operand:		//number, date, string
					item.type = nItemType;
					item.itemValue = oValue;
					item.position = nPosition;
					aPolscaFormula.Add(item);
					break;
                case ItemType.VariableValue:		//variable value
                    item.type = nItemType;
                    item.itemValue = oValue;
                    item.position = nPosition;
                    item.index = nIndex;
                    aPolscaFormula.Add(item);
                    break;
            }
		}
		
		/// <summary>
		/// Adds item to Polish Notation array.
		/// </summary>
		/// <param name="nItemType">Item type.</param>
		/// <param name="nIndex">Item index in corresponding collection.</param>
		/// <param name="nPosition">Position in an expression string.</param>
		/// <param name="aPolscaFormula">Polish Notation array.</param>
		/// <param name="aStack">Auxiliary stack of functions. Is used to build a Polish Notation array.</param>
		/// <remarks>Overloaded version, is used to add variables, functions and operators.</remarks>
		private void AddToPolsca(ItemType nItemType, int nIndex, int nPosition, ref ArrayList aPolscaFormula, ref ArrayList aStack)
		{
			// adds item to polish notation
			ItemInfo item = new ItemInfo();
			ItemInfo stackItem = new ItemInfo();

			switch (nItemType)
			{
				case ItemType.Constant:		//constant
					item.type = nItemType;					
					item.index = -1; // SVA.2827 to ensure that error is raised when attempted to use index //item.index = nIndex;
					item.position = nPosition;
					item.itemValue = m_cConstants[nIndex];
					aPolscaFormula.Add(item);

					break;

				case ItemType.Variable:		//variable
					item.type = nItemType;
					item.index = nIndex;
					item.position = nPosition;
					aPolscaFormula.Add(item);
					break;

				case ItemType.Function:		//function
					item.type = nItemType;
					item.index = -1; // SVA.2827 to ensure that error is raised when attempted to use index //item.index = nIndex;
					item.position = nPosition;
					item.itemValue = m_cFunctions[ nIndex ];
					aStack.Add(item);

					break;

				case ItemType.Operator:		//operator
					
					if (m_cOperators[nIndex].OperandsSupported > 1)	// add unary operators to stack, like functions
					{
						bool needExit = false;
						do
						{
							if (aStack.Count == 0) needExit = true;
							else 
							{
								stackItem = ((ItemInfo) aStack[aStack.Count-1]);
								switch (stackItem.type)
								{
									case ItemType.Function: //always get functions from stack
										aPolscaFormula.Add (stackItem);
										aStack.RemoveAt(aStack.Count-1);
										break;
									case ItemType.Operator:
										//if (m_cOperators[stackItem.index].GetPriority() >= m_cOperators[nIndex].GetPriority())
										if (stackItem.GetOperator().GetPriority() >= m_cOperators[nIndex].GetPriority())
										{
											aPolscaFormula.Add (stackItem);
											aStack.RemoveAt(aStack.Count-1);
										}
										else needExit = true;
										break;
									default:
										needExit = true;
										break;
								}
							}
						}
						while (!needExit);
					}

					item.type = nItemType;
					item.index = -1;// SVA.2827 to ensure that error is raised when attempted to use index  //item.index = nIndex;
					item.position = nPosition;
					item.itemValue = m_cOperators[nIndex];
					aStack.Add(item);

					break;

			}
		}
		
		/// <summary>
		/// Add item to Polish Notation array.
		/// </summary>
		/// <param name="nItemType">Item type.</param>
		/// <param name="aPolscaFormula">Polish Notation array.</param>
		/// <param name="aStack">Auxiliary stack of functions. Is used to build a Polish Notation array.</param>
		/// <remarks>Overloaded version, is used to add parentheses, parentheses and commas.</remarks>
		private void AddToPolsca(ItemType nItemType, ref ArrayList aPolscaFormula, ref ArrayList aStack)
		{
			// adds item to polish notation
			ItemInfo item = new ItemInfo();
			ItemInfo stackItem = new ItemInfo();

			switch (nItemType)
			{
				case ItemType.OpeningParen:	// '('
					item.type = ItemType.OpeningParen;
					aStack.Add(item);
					break;

				case ItemType.ClosingParen:	// ')'
					stackItem = (ItemInfo) aStack[aStack.Count-1];
					if (aStack.Count > 0 && (stackItem.type != ItemType.OpeningParen))
					{
						do
						{
							aPolscaFormula.Add(stackItem);

							aStack.RemoveAt(aStack.Count-1);
							stackItem = (ItemInfo) aStack[aStack.Count-1];
						}
						while (stackItem.type != ItemType.OpeningParen);
					};
					aStack.RemoveAt(aStack.Count-1); // del '(' from stack
					break;

				case ItemType.Comma:			// ',' get values from stack 
					stackItem = (ItemInfo) aStack[aStack.Count-1];
					if (aStack.Count > 0 && (stackItem.type != ItemType.OpeningParen))
					{
						do
						{
							aPolscaFormula.Add(stackItem);

							aStack.RemoveAt(aStack.Count-1);
							stackItem = (ItemInfo) aStack[aStack.Count-1];
						}
						while (stackItem.type != ItemType.OpeningParen);
					};
					break;
			}
		}
		
		/// <summary>
		/// Returns a value indicating whether the occurrence of the given token is valid so that it could be pushed to the Polish Notation stack.
		/// </summary>
		/// <param name="newType">Type of the new token.</param>
		/// <param name="operandsSupported">Number of operands supported.</param>
		/// <param name="lastType">Type of the preceding token.</param>
		/// <returns>True, if the occurrence is valid; otherwise, false.</returns>
		/// <remarks>Overloaded version to check if operator is valid.</remarks>
		private bool CanAdd(ItemType newType, int operandsSupported, ref ItemType lastType)
		{
			if (operandsSupported == 1)
			{
				if (lastType == ItemType.OpeningParen || lastType == ItemType.Comma || lastType == ItemType.Operator)
				{
					lastType = ItemType.Operator;
					return true;
				}
			}
			else
			{
				if (lastType == ItemType.Variable || lastType == ItemType.ClosingParen || lastType == ItemType.Constant || lastType == ItemType.Operand)
				{
					lastType = ItemType.Operator;
					return true;
				}
			}
						
			return false;
		}

		/// <summary>
		/// Returns a value indicating whether the occurrence of the given token is valid so that it could be pushed to the Polish Notation stack.
		/// </summary>
		/// <param name="newType">Type of the new token.</param>
		/// <param name="lastType">Type of the preceding token.</param>
		/// <param name="nPosition">Current position in an expression text.</param>
		/// <param name="bImplicitMultiplication">Value indicating whether implicit multiplication is supported.</param>
		/// <param name="aPolscaFormula">Polish Notation stack.</param>
		/// <param name="aStack">Auxiliary stack of functions. Is used to build a Polish Notation array.</param>
		/// <returns>True, if new item can be added; otherwise, false.</returns>
		private bool CanAdd(ItemType newType, ref ItemType lastType, int nPosition, bool bImplicitMultiplication, ref ArrayList aPolscaFormula, ref ArrayList aStack)
		{
			switch (newType)
			{
				case ItemType.Operand:
					if (lastType == ItemType.OpeningParen || lastType == ItemType.Comma || lastType == ItemType.Operator)
					{
						lastType = newType;
						return true;
					}
					if (bImplicitMultiplication)
					{
						if (lastType == ItemType.ClosingParen)
						{
							// expression like "(...)3"
							// add MultiplyOperator
							AddToPolsca(ItemType.Operator, (int) OperatorType.multiplyOperator, nPosition, ref aPolscaFormula, ref aStack);
							lastType = newType;
							return true;
						}
					}
					break;

				case ItemType.Constant:
				case ItemType.Variable:
				case ItemType.Function:
					if (lastType == ItemType.OpeningParen || lastType == ItemType.Comma || lastType == ItemType.Operator)
					{
						lastType = newType;
						return true;
					}
					if (bImplicitMultiplication)
					{
						if (lastType == ItemType.Operand || lastType == ItemType.ClosingParen)
						{
							// expression like "3x1", "(...)x1", "3PI", "(...)PI",  "3sin(x)" or "(...)sin(x)"
							// add MultiplyOperator
							AddToPolsca(ItemType.Operator, (int) OperatorType.multiplyOperator, nPosition, ref aPolscaFormula, ref aStack);
							lastType = newType;
							return true;
						}
					}
					break;

				case ItemType.OpeningParen:
					if (lastType == ItemType.OpeningParen || lastType == ItemType.Comma || lastType == ItemType.Operator || lastType == ItemType.Function)
					{
						lastType = newType;
						return true;
					}
					if (bImplicitMultiplication)
					{
                        if (lastType == ItemType.Operand || lastType == ItemType.Variable || lastType == ItemType.VariableValue || lastType == ItemType.Constant || lastType == ItemType.ClosingParen)
						{
							// expression like "3(...)", "x1(...)", "PI(...)" or "(...)(...)"
							// add MultiplyOperator
							AddToPolsca(ItemType.Operator, (int) OperatorType.multiplyOperator, nPosition, ref aPolscaFormula, ref aStack);
							lastType = newType;
							return true;
						}
					}
					break;

				case ItemType.Comma:
				case ItemType.ClosingParen:
					if (lastType == ItemType.ClosingParen || lastType == ItemType.Variable || lastType == ItemType.Constant || lastType == ItemType.Operand)
					{
						lastType = newType;
						return true;
					}
					break;
			}
			return false;
		}

	}

	/// <summary>
	/// Enumerates available token types
	/// </summary>
	internal enum ItemType 
	{
		OpeningParen = 0,		// (
		ClosingParen,			// )
		Comma,					// ,
			
		Operand,				// Number, date or string
		Constant,				// Constant
		Variable,				// Variable
        Function,				// Function
		Operator,				// Operator
        VariableValue,			// VariableValue
    }
		

	/// <summary>
	/// Represents a token item. Tokens are stored in Polish Notation array.
	/// </summary>
	internal struct ItemInfo
	{
		public ItemType type;
		public int position;			// position in an exprssion string being parsed
		public int index;				// only for variables
		public int paramCount;			// only for functions
		public object itemValue;		// for numbers, dates and strings - value
										// for functions, constants and operators - object
		public Function GetFunction()
		{
			return (Function) itemValue;
		}
		public Operator GetOperator()
		{
			return (Operator) itemValue;
		}
		public Constant GetConstant()
		{
			return (Constant) itemValue;
		}
	}

	/// <summary>
	/// Represents an item in Function Stack. 
	/// </summary>
	internal struct FunctionInfo
	{
		public int stackIndex;		//index in aStack Array
		public int parentCount;		//number of "(" before function
		public int paramCount;		//number of passed parameters
	}

}
