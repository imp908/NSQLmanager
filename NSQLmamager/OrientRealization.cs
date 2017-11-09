﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;

using WebManagers;
using IQueryManagers;
using QueryManagers;

using IOrientObjects;

using IJsonManagers;
using IWebManagers;

using POCO;

/// <summary>
/// Realization of IJsonMangers, IWebManagers, and IOrient specifically for orient db
/// </summary>
namespace OrientRealization
{

    /// <summary>
    ///     Orient specific WebManager for authentication and authenticated resopnses sending to URL
    ///     with NetworkCredentials
    /// </summary>    

    public class OrientWebManager : WebManager
    {
        //>> add async
        public new HttpWebResponse GetResponse(string url, string method)
        {

            //HttpWebResponse resp = null;
            base.addRequest(url, method);
            addHeader(HttpRequestHeader.Cookie, this.OSESSIONID);
            try
            {
                return (HttpWebResponse)this._request.GetResponse();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
            }

            return null;
        }
        public WebResponse Authenticate(string url, NetworkCredential nc=null)
        {

            WebResponse resp;
            addRequest(url, "GET");
		    if (nc == null) { bindCredentials();  }
            else { addCredentials(nc); }
            try
            {
                resp = this._request.GetResponse();
                OSESSIONID = getHeaderValue("Set-Cookie");
                return resp;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }

   
    /// <summary>
    ///  Tokens realization for different string concatenations
    /// </summary>
    
    //Tokens for Orient Comamnd and Authenticate URLs
    public class OrientHost : ITypeToken
    {
        public string Text { get { return "http://msk1-vm-ovisp02"; } set { Text = value; } }
    }
    public class OrientDb : ITypeToken
    {
        public string Text { get; set; } = "Orgchart_prod";
    }
    public class OrientPort : ITypeToken
    {
        public string Text { get { return "2480"; } set { Text = value; } }
    }
    public class OrientAuthenticateToken : ITypeToken
    {
        public string Text { get { return "connect"; } set { Text = value; } }
    }
    public class OrientCommandToken : ITypeToken
    {
        public string Text { get { return "command"; } set { Text = value; } }
    }
    public class OrientCommandSQLTypeToken : ITypeToken
    {
        public string Text { get { return "sql"; } set { Text = value; } }
    }
    public class OrientFuncionToken : ITypeToken
    {
        public string Text { get { return "Function"; } set { Text = value; } }
    }
    public class OrientBatchToken : ITypeToken
    {
        public string Text { get { return "Batch"; } set { Text = value; } }
    }


    /// <summary>
    /// Orient query tokens
    /// </summary>  

    //Orient SQL syntax tokens    
    public class OrientSelectToken : ITypeToken
    {
        public string Text { get; set; } = "Select";
    }
    public class OrientFromToken : ITypeToken
    {
        public string Text { get; set; } = "from";
    }
    public class OrientWhereToken : ITypeToken
    {
        public string Text { get; set; } = "where";
    }

    public class OrientCreateToken : ITypeToken
    {
        public string Text { get; set; } = "Create";
    } 
    public class OrientContentToken : ITypeToken
    {
        public string Text { get; set; } = "content";
    }
    public class OrientDeleteToken : ITypeToken
    {
        public string Text { get; set; } = "Delete";
    }
		 public class OrientDropToken : ITypeToken
    {
        public string Text { get; set; } = "Drop";
    }


    public class OrientClassToken :ITypeToken
    {
        public string Text { get; set; } = "Class";
    }
public class OrientVertexToken : ITypeToken
    {
        public string Text { get; set; } = "Vertex";
    }
    public class OrientEdgeToken : ITypeToken
    {
        public string Text { get; set; } = "Edge";
    }
    public class OrientPropertyToken : ITypeToken
    {
        public string Text { get; set; } = "Property";
    }
	  public class OrientToToken : ITypeToken
    {
        public string Text { get; set; } = "To";
    }

    public class OrientExtendsToken : ITypeToken
    {
        public string Text { get; set; } = "Extends";
    }
	  public class OrientMandatoryToken : ITypeToken
    {
        public string Text { get; set; } = "MANDATORY";
    }
    public class OrientNotNULLToken : ITypeToken
    {
        public string Text { get; set; } = "notnull";
    } 
    public class OrientDotToken : ITypeToken
    {
        public string Text { get; set; } = ".";
    }
	public class OrientComaToken : ITypeToken
    {
        public string Text { get; set; } = @",";
    }
	public class OrientLtRoundSquareToken : ITypeToken											  
    {
        public string Text { get; set; } = @"(";
    }
    public class OrientRtRoundSquareToken : ITypeToken
    {
        public string Text { get; set; } = @")";
    }
    public class OrientTRUEToken : ITypeToken
    {
        public string Text { get; set; } = @"TRUE";
    }
    public class OrientFLASEToken : ITypeToken
    {
        public string Text { get; set; } = @"FALSE";
    }
	
    public class OrientIdSharpToken : ITypeToken
    {
        public string Text { get; set; } = @"#";
    }
    public class OrientIdToken : ITypeToken
    {
        public string Text { get; set; } = "Id";
    }
    public class OrientNameToken : ITypeToken
    {
        public string Text { get; set; } = "Name";
    }
    public class OrientAccountToken : ITypeToken
    {
        public string Text { get; set; } = "sAMAccountName";
    }

    //orient dateformats tokens
    public class OrientSTRINGToken : ITypeToken
    {
        public string Text { get; set; } = "STRING";
    }
    public class OrientDATEToken : ITypeToken
    {
        public string Text { get; set; } = "DATE";
    }
    public class OrientINTEGERToken : ITypeToken
    {
        public string Text { get; set; } = "INTEGER";
    }
    public class OrientPersonToken : ITypeToken
    {
        public string Text { get; set; } = "Person";
    }
    public class OrientUnitToken : ITypeToken
    {
        public string Text { get; set; } = "Unit";
    }
    public class OrientSubUnitToken : ITypeToken
    {
        public string Text { get; set; } = "SubUnit";
    }
    public class OrientMainAssignmentToken : ITypeToken
    {
        public string Text { get; set; } = "MainAssignment";
    }    
    public class OrientDATETIMEToken : ITypeToken
    {
        public string Text { get; set; } = "DATETIME";
    }
	 public class OrientUserSettingsToken : ITypeToken
    {
        public string Text { get; set; } = "UserSettings";
    }
    public class OrientCommonSettingsToken : ITypeToken
    {
        public string Text { get; set; } = "CommonSettings";
    }

    public class OrientBrewery : ITypeToken
    {
        public string Text { get; set; } = "Brewery";
    }

    ///<summary>
    ///Web response reader Tokens
    /// </summary>

    public class RESULT : ITypeToken
    {
        public string Text { get; set; } = "result";
    }


    /// <summary>
    /// Builder formats
    /// </summary>

    public class OrientTokenFormatFromListGenerate : ITypeToken
    {
        public string Text { get; set; }
        public OrientTokenFormatFromListGenerate(List<ITypeToken> tokens)
        {
            string res = "{}";
            int[] cnt = new int[tokens.Count];            
            for(int i =0;i<tokens.Count();i++)
            {
                cnt[i] = i;
            }
            string res2 = string.Join(@"} {", cnt);
            this.Text = res.Insert(1, res2);
        }
    }
    //Auth Orient URL
    public class OrientAuthenticationURLFormat : ITypeToken
    {
        public string Text { get { return @"{0}:{1}/{2}/{3}"; } set { Text = value; } }
    }
    //Command URL part format
    public class OrientCommandURLFormat : ITypeToken
    {
        public string Text { get { return @"{0}:{1}/{2}/{3}/{4}"; } set { Text = value; } }
    }

    /// </summary>
    /// command queries contains prevoius command as first parameter, 
    /// cause WHERE not intended to be used without select
    /// </summary>
    //command query part format
    public class OrientSelectClauseFormat : ITypeToken
    {
        public string Text { get { return @"{0} {1} {2}"; } set { Text = value; } }
    }
    //Command for concatenating select command and where clause
    public class OrientWhereClauseFormat : ITypeToken
    {
        public string Text { get { return @"{0} {1}"; } set { Text = value; } }
    }
    //create vertex command Format
    public class OrientCreateVertexCluaseFormat : ITypeToken
    {
        public string Text { get { return @"{0} {1} {2} {3} {4}"; } set { Text = value; } }
    }
    //delete vertex command Format
    public class OrientDeleteVertexCluaseFormat : ITypeToken
    {
        public string Text { get { return @"{0} {1} {2} {3}"; } set { Text = value; } }
    }
    //delete command Format
    public class OrientDeleteCluaseFormat : ITypeToken
    {
        public string Text { get { return @"{0} {1} {2}"; } set { Text = value; } }
    }


    /// <summary>
    /// Builders.
    /// Build command acording to type of passed object (class,vertes, or edge with objects referenced or ids)
    /// Not use predefined formatters 
    /// for special commands like create class/edge/vertex
    /// which not requer special format like {0}:{1}\{2} but samle , generated fro mtoken list like {0} {1} {2}
    /// but generated in lagre ammounts with differen types.
    /// </summary>

    //implicit token builder
    public class OrientTokenBuilder : ITokenBuilder
    {

        public ITypeConverter typeConverter;

        //for create (Class,Vertex) command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientContext=null)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            if (command_ is OrientCreateToken){                
                result.Add(command_);
                result.Add(orientObject);
                result.Add(typeConverter.Get(orientObject));               
          
                if(orientContext!=null)
                {
                    if(orientObject is OrientClass)
                    {
                        result.Add(new OrientExtendsToken());
                    }
                    if (orientObject is OrientVertex)
                    {
                        result.Add(new OrientContentToken());
                        result.Add(orientContext);
                    }
                }
            }
            if (command_ is OrientDeleteToken)
            {
                result.Add(command_);
                if (orientObject is OrientClass)
                {
                    result.Add(new OrientClassToken());
                    result.Add(typeConverter.Get(orientObject));
                }
                if (orientObject is OrientVertex || orientObject is OrientEdge)
                {
                    result.Add(typeConverter.GetBase(orientObject));
                    result.Add(typeConverter.Get(orientObject));
                    if(orientContext!=null)
                    {
                        result.Add(new OrientWhereToken());
                        result.Add(orientContext);
                    }
                }
            }
            return result;
        }
        //edge add
        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientObject, IOrientObject from, IOrientObject to, ITypeToken orientContext = null)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            if (command_ is OrientCreateToken)
            {
                result.Add(command_);
                if (orientObject is OrientEdge)
                {
                    result.Add(typeConverter.GetBase(orientObject));
                    result.Add(typeConverter.Get(orientObject));
                    result.Add(new OrientFromToken());
                    result.Add(typeConverter.Get(from));
                    result.Add(new OrientToToken());
                    result.Add(typeConverter.Get(to));
                    if(orientContext!=null)
                    {
                        result.Add(new OrientContentToken());
                        result.Add(orientContext);
                    }

                }
            }
            return result;
        }
        //add edge with IDs
        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientObject, ITypeToken from, ITypeToken to, ITypeToken orientContext = null)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            if (command_ is OrientCreateToken)
            {
                result.Add(command_);
                if (orientObject is OrientEdge)
                {
                    result.Add(typeConverter.GetBase(orientObject));
                    result.Add(typeConverter.Get(orientObject));
                    result.Add(new OrientFromToken());
                    result.Add(from);
                    result.Add(new OrientToToken());
                    result.Add(to);
                    if (orientContext != null)
                    {
                        result.Add(new OrientContentToken());
                        result.Add(orientContext);
                    }

                }
            }
            return result;
        }
        //property add
        public List<ITypeToken> Command(ITypeToken command_, IOrientObject orientObject, IOrientObject orientClass_, ITypeToken orientTypeClass_, bool mandatory = false, bool notNull = false)
        {
            List<ITypeToken> result = new List<ITypeToken>();

            if (command_ is OrientCreateToken && orientObject is OrientProperty && orientClass_ is OrientClass)
            {
                result.Add(command_);
                result.Add(new OrientPropertyToken());
                result.Add(typeConverter.Get(orientObject));
                result.Add(new OrientDotToken());
                result.Add(typeConverter.Get(orientClass_));
            }
            if(mandatory)
            {
                //add itypetokens
            }
            return result;
        }
       
    }
	
	/// <summary>creates collection of tokens
    //builds add,delete,create commands from token amount
    /// </summary>
    public class OrientTokenBuilderImplicit : ITokenBuilderImplicit
    {
        //for select command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            result.Add(command_);
            result.Add(new OrientFromToken());
            result.Add(orientObject);
            return result;
        }

        //for delete, select conditional command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            if (command_ is OrientDeleteToken)
            {
                result.Add(command_);
                result.Add(orientType);
                //result.Add(new OrientFromToken());
		  
									   
							 
					 
                result.Add(orientObject);
												  
					 
				 
            }
            if (command_ is OrientSelectToken)
            {
                result.Add(command_);
												
				 
                result.Add(new OrientFromToken());
                result.Add(orientObject);                
				 
																			   
				 
																	
																
										   
					 
                result.Add(new OrientWhereToken());
                result.Add(orientType);
					 
				 
            }
            return result;
        }

        //for create command, authenticate, command
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken context_)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            result.Add(command_);
            result.Add(orientType);
            result.Add(orientObject);
            result.Add(new OrientContentToken());
            result.Add(context_);
            return result;
        }

        //for create Edge from to 
        public List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType,  ITypeToken tokenA, ITypeToken tokenB, ITypeToken context_)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            result.Add(command_);
            result.Add(orientType);
            result.Add(orientObject);
            result.Add(new OrientFromToken());
            result.Add(tokenA);
            result.Add(new OrientToToken());
            result.Add(tokenB);
            result.Add(new OrientContentToken());
            result.Add(context_);
            return result;
        }

    }
    
	
	/// <summary>
    /// Builder with explicitly named commands
    /// </summary>
    public class OrientTokenBuilderExplicit
    {      

        //Create class cluase (type check) with extends class option
        public List<ITypeToken> Create(OrientClassToken classType_, ITypeToken extendsClassType_=null)
        {

            List<ITypeToken> result = new List<ITypeToken>() {
                new OrientCreateToken()
            };

            if (classType_ is OrientClassToken)
            {
                result.Add(new OrientClassToken());
											   									  
                result.Add(classType_);
													
                if (extendsClassType_ != null)
                {
                    result.Add(new OrientExtendsToken());
                    result.Add(extendsClassType_);
                }
            }           
            return result;
        }
        //Create  vertex cluase (type check) with content optiona
        public List<ITypeToken> Create(OrientVertexToken classType_, ITypeToken extendsClassType_=null)
        {
            List<ITypeToken> result = new List<ITypeToken>() {
                new OrientCreateToken()
            };
            if (classType_ is OrientVertexToken)
            {
                result.Add(new OrientVertexToken());
											   
								  
                result.Add(classType_);

                if (extendsClassType_ != null)
                {
                    result.Add(new OrientContentToken());
                    result.Add(extendsClassType_);
                }
            }
            return result;
        }
        //Create property cluase
        public List<ITypeToken> Create(ITypeToken className_, ITypeToken propertyName_, ITypeToken propertyType_,
        bool mandatory_ = false, bool notnull=false)
        {
            List<ITypeToken> result = new List<ITypeToken>() {
                new OrientCreateToken(), new OrientPropertyToken(),className_, new OrientDotToken(),propertyName_,propertyType_
            };

            result.Add(new OrientLtRoundSquareToken());
            result.Add(new OrientMandatoryToken());

            if (mandatory_){
                result.Add(new OrientTRUEToken());
            }else { result.Add(new OrientFLASEToken()); }

            result.Add(new OrientComaToken());
            result.Add(new OrientNotNULLToken());

            if (notnull){
                result.Add(new OrientTRUEToken());
            }else { result.Add(new OrientFLASEToken()); }

            result.Add(new OrientRtRoundSquareToken());

            return result;
        }
        //Create edge clause
        public List<ITypeToken> Create (ITypeToken className_, ITypeToken from_,ITypeToken to_)
        {

            List<ITypeToken> result = new List<ITypeToken>(){
                new OrientCreateToken(), new OrientEdgeToken(), className_, new OrientFromToken(),
                from_,
                new OrientToToken(),
                to_
            };

            return result;
        }
        //For select from cluases (Vertex,edge)
        public List<ITypeToken> Select(ITypeToken orientType_, ITypeToken class_)
        {
            List<ITypeToken> result = new List<ITypeToken>() {
                new OrientSelectToken(),orientType_, new OrientFromToken(), class_
            };

            return result;
        }
        //Where clauses receiveing content< which must be strictly parametrized in UOW
        public List<ITypeToken> Where(ITypeToken orientType_, ITypeToken content_)
        {
            List<ITypeToken> result = new List<ITypeToken>() {
                new OrientWhereToken(),content_
            };

            return result;
        }
        //delete vertex,edge,or class
        public List<ITypeToken> Delete(ITypeToken classType_,ITypeToken classname_)
        {
            List<ITypeToken> result = new List<ITypeToken>();
            if (classType_ is OrientVertexToken)
            {
                result.Add(new OrientDeleteToken());
													  
                result.Add(classType_);
												 
															
            }
            if (classType_ is OrientEdgeToken)
            {
                result.Add(new OrientDeleteToken());
                result.Add(classType_);
            }
            if (classType_ is OrientClassToken)
            {
                result.Add(new OrientDropToken());
                result.Add(classType_);
            }
            if(result.Count()!=0)
            {
                result.Add(classname_);
            }
            return result;
        }		

    }
    
	
    //buider for commands with format
    //mostly used for URLS (auth,
    public class OrientCommandBuilder : Textbuilder
    {
        public OrientCommandBuilder() : base()
        {

        }
        public OrientCommandBuilder(List<ITypeToken> tokens_, ITypeToken FormatPattern_)
             : base(tokens_, FormatPattern_)
        {

        }
    }
	
	

    //Authentication URL build
    public class OrientAuthenticationURIBuilder : Textbuilder
    {
        public OrientAuthenticationURIBuilder(List<ITypeToken> tokens_, OrientAuthenticationURLFormat FormatPattern_)
             : base(tokens_, FormatPattern_)
        {

        }
    }
    //Command URL build
    public class OrientCommandURIBuilder : Textbuilder
    {
        public OrientCommandURIBuilder(List<ITypeToken> tokens_, OrientCommandURLFormat FormatPattern_)
            : base(tokens_, FormatPattern_)
        {

        }
        public OrientCommandURIBuilder(List<ITextBuilder> texts_, ITypeToken FormatPattern_, Textbuilder.BuildTypeFormates type_)
          : base(texts_, FormatPattern_, type_)
        {

        }
    }

    //<<--deprecatoin possible, replaced with type convertible commandbuilder
    //Query builders
    //class segregation for different cluse builders
    public class OrientSelectClauseBuilder : Textbuilder
    {
        public OrientSelectClauseBuilder(List<ITypeToken> tokens_, OrientSelectClauseFormat FormatPattern_ = null)
            : base(tokens_, FormatPattern_ = new OrientSelectClauseFormat())
        {

        }
    }
    public class OrientWhereClauseBuilder : Textbuilder
    {
        public OrientWhereClauseBuilder(List<ITypeToken> tokens_, OrientWhereClauseFormat FormatPattern_)
            : base(tokens_, FormatPattern_)
        {

        }
    }

    public class OrientCreateClauseBuilder : Textbuilder
    {
        public OrientCreateClauseBuilder(List<ITypeToken> tokens_, ITypeToken format_)
            : base(tokens_, format_)
        {

        }
    }
    public class OrientDeleteClauseBuilder : Textbuilder
    {
        public OrientDeleteClauseBuilder(List<ITypeToken> tokens_, ITypeToken format_)
            : base(tokens_, format_)
        {

        }
    }


     
    //predefined url token collections
    //prefered change to predefined url and command builds
    public static class TokenRepo
    {
        public static List<ITypeToken> authUrl = new List<ITypeToken>() { new OrientHost(), new OrientPort(), new OrientAuthenticateToken(), new OrientDb() };
        public static List<ITypeToken> commandUrl = new List<ITypeToken>() { new OrientHost(), new OrientPort(), new OrientCommandToken(), new OrientDb(), new OrientCommandSQLTypeToken() };
    }

    //converts from token tyoes for orient Db objects like (vertex,edge) to according model POCO class
    //Vertex (Person) to class (Person)    
    public class TypeConverter : ITypeConverter
    {
        Dictionary<Type, ITypeToken> types;

        public TypeConverter()
        {
            types = new Dictionary<Type, ITypeToken>();

            types.Add(typeof(OrientVertex), new OrientVertexToken());
            types.Add(typeof(OrientEdge), new OrientEdgeToken());

            types.Add(typeof(Person), new OrientPersonToken());
            types.Add(typeof(Unit), new OrientUnitToken());

            types.Add(typeof(MainAssignment), new OrientMainAssignmentToken());
            types.Add(typeof(SubUnit), new OrientSubUnitToken());
        }
        public void Add(Type type_, ITypeToken token_)
        {
            this.types.Add(type_, token_);
        }
        public ITypeToken Get(Type type_)
        {
            ITypeToken token_ = null;

            types.TryGetValue(type_, out token_);

            return token_;
        }
        public ITypeToken GetBase(Type type_)
        {
            ITypeToken token_ = null;
            Type tp = type_.BaseType;
            types.TryGetValue(tp, out token_);

            return token_;
        }
        public ITypeToken Get(IOrientObject object_)
        {
            ITypeToken token_ =null;
            
                types.TryGetValue(object_.GetType(), out token_);
                         
            return token_;
        }
        public ITypeToken GetBase(IOrientObject object_)
        {
            ITypeToken token_ = null;
            Type tp = object_.GetType().BaseType;
            IOrientObject t2 = (IOrientObject)object_;

                types.TryGetValue(object_.GetType().BaseType, out token_);
            

            return token_;
        }
    }
		
	
    //boilderplate usage
    //rewrite to command builder use
    public class OrientRepo
    {

        IJsonManger jm;
        ITokenBuilder tb;
        ITypeConverter tk;
        ITextBuilder txb;
        IWebManager wm;
        IResponseReader ir;

        OrientWebManager owm = new OrientWebManager();

        string AuthUrl;
        string CommandUrl;
        string queryUrl;

        public OrientRepo(
            IJsonManger jsonManager_,ITokenBuilder tokenBuilder_,ITypeConverter typeConverter_,ITextBuilder textBuilder_, 
            IWebManager webManger_, IResponseReader responseReader_)
        {
            this.jm = jsonManager_;
            this.tb = tokenBuilder_;
            this.tk = typeConverter_;
            this.txb = textBuilder_;
            this.wm = webManger_;
            this.ir = responseReader_;  

            AuthUrl =txb.Build(TokenRepo.authUrl, new OrientAuthenticationURLFormat());
            CommandUrl= txb.Build(TokenRepo.commandUrl, new OrientCommandURLFormat());
            
        }

        public string Add(IOrientObject obj_)
        {

            string content = jm.SerializeObject(obj_);
            List<ITypeToken> commandTk =  tb.Command(new OrientCreateToken(), obj_, obj_, new TextToken() { Text= content});
            string command = txb.Build(commandTk, new OrientCreateVertexCluaseFormat());
            queryUrl = CommandUrl + "/" + command;

            owm.Authenticate(AuthUrl, 
                new NetworkCredential(ConfigurationManager.AppSettings["ParentLogin"], ConfigurationManager.AppSettings["ParentPassword"]));   

            string resp =
            ir.ReadResponse(
                owm.GetResponse(queryUrl, new POST().Text)
               );

            return resp;

        }
        public string Add(IOrientObject obj_, IOrientObject from, IOrientObject to)
        {
            
            string content = jm.SerializeObject(obj_);
            List<ITypeToken> commandTk = tb.Command(new OrientCreateToken(),from,to,new TextToken() { Text=content});
       
            string command = txb.Build(commandTk, new OrientTokenFormatFromListGenerate(commandTk) );
            queryUrl = CommandUrl + "/" + command;

            owm.Authenticate(AuthUrl,
                new NetworkCredential(ConfigurationManager.AppSettings["ParentLogin"], ConfigurationManager.AppSettings["ParentPassword"]));

            string resp =
            ir.ReadResponse(
                owm.GetResponse(queryUrl, new POST().Text)
               );

            return resp;

        }
        public string Add(IOrientObject obj_, ITypeToken from, ITypeToken to)
        {

            string content = jm.SerializeObject(obj_);
            List<ITypeToken> commandTk = tb.Command(new OrientCreateToken(), obj_, from, to, new TextToken() { Text = content });

            string command = txb.Build(commandTk, new OrientTokenFormatFromListGenerate(commandTk));
            queryUrl = CommandUrl + "/" + command;

            owm.Authenticate(AuthUrl,
                new NetworkCredential(ConfigurationManager.AppSettings["ParentLogin"], ConfigurationManager.AppSettings["ParentPassword"]));

            string resp =
            ir.ReadResponse(
                owm.GetResponse(queryUrl, new POST().Text)
               );

            return resp;

        }
        public string Delete(Type type_ ,ITypeToken condition_)
        {
            string deleteClause;
                       
            List<ITypeToken> commandTk = tb.Command(new OrientDeleteToken(), tk.Get(type_));
            List<ITypeToken> whereTk = new List<ITypeToken>() { new OrientWhereToken(), new TextToken() { Text = @"Name = 0" } };

            deleteClause = txb.Build(commandTk, new OrientDeleteCluaseFormat());
            string whereClause = txb.Build(whereTk, new OrientWhereClauseFormat());

            queryUrl = CommandUrl + "/" + deleteClause + " " + whereClause;

            owm.Authenticate(AuthUrl,
               new NetworkCredential(ConfigurationManager.AppSettings["ParentLogin"], ConfigurationManager.AppSettings["ParentPassword"]));

            string resp =
            ir.ReadResponse(
                owm.GetResponse(queryUrl, new POST().Text)
               );

            return resp;

        }

        public string Select(Type object_, ITypeToken condition_)
        {

            string result = null;


            List<ITypeToken> commandTk = tb.Command(new OrientSelectToken(), tk.Get(object_), condition_);
            string command = txb.Build(commandTk, new OrientTokenFormatFromListGenerate(commandTk));
            queryUrl = CommandUrl + "/" + command;

            owm.Authenticate(AuthUrl);


            result =
            ir.ReadResponse(
                owm.GetResponse(queryUrl, new GET().Text)
            );

            return result;

        }

    }

}