using System;
using System.Collections.Generic;
using System.Data.OleDb;


namespace DataBase_Interface
{
    public class ArticleDAL
    {
        /*_ ATTRIBUTE _*/
        static OleDbConnection db;


        /*_ CONNECTION _*/
        public static void ConnectTo(string DataBaseFile)
        {
            string connectionInfo = @"Provider=Microsoft.ACE.OLEDB.12.0 ; Data Source=" + DataBaseFile;

            OleDbConnection connection = new OleDbConnection(connectionInfo); //_ Connection Object.
            db = connection;

            try //_ Connection Opening.
            {
                db.Open();
                Console.WriteLine("Connection Established Successfully..");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void DisconnectFromDB()
        {
            try //_ Connection Closing.
            {
                db.Close();
                Console.WriteLine("DataBase Disconnected ..");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /*_ DB_ACTIONS _*/
        public static void Add(Article a)
        {
            string addQuery = "INSERT INTO Article VALUES (?, ?, ?)";
            OleDbCommand addCommand = new OleDbCommand(addQuery, db);    //_ Command Object.

            addCommand.Parameters.Add(new OleDbParameter("Reference", a.Reference));     //_ Paramters Adding (Exceptions Managed by code).
            addCommand.Parameters.Add(new OleDbParameter("Designation", a.Designation));
            addCommand.Parameters.Add(new OleDbParameter("Price", a.Price));

            try //_ Query Execution.
            {
                addCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void Update(int Ref, Article a)
        {
            string updateQuery = "UPDATE Article SET Reference=?, Designation=?, Prix=? WHERE Reference=?";
            OleDbCommand updateCommand = new OleDbCommand(updateQuery, db);    //_ Command Object.

            updateCommand.Parameters.Add(new OleDbParameter("Reference", a.Reference)); //_ Parameters Adding (Exceptions Managed by the code).
            updateCommand.Parameters.Add(new OleDbParameter("Designation", a.Designation));
            updateCommand.Parameters.Add(new OleDbParameter("Prix", a.Price));
            updateCommand.Parameters.Add(new OleDbParameter("ReferenceBase", Ref));

            try //_ Query Execution.
            {
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void Delete(int Ref)
        {
            string deleteQuery = "DELETE FROM Article WHERE Reference=?";
            OleDbCommand deleteCommand = new OleDbCommand(deleteQuery, db);  //_ Command Object.

            deleteCommand.Parameters.Add(new OleDbParameter("Reference", Ref));

            try //_ Query Execution.
            {
                deleteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static Article SelectByRef(int Ref)
        {
            string referenceSelectQuery = "SELECT * FROM Article WHERE Reference=?";
            OleDbCommand referenceSelectCommand = new OleDbCommand(referenceSelectQuery, db);    //_ Command Object.

            referenceSelectCommand.Parameters.Add(new OleDbParameter("Reference", Ref));

            OleDbDataReader ArticleRead = null;   //_ Structures Receiving Object.
            try //_ Query Execution.
            {
                ArticleRead = referenceSelectCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Article searchedArticle = null;     //_ Instanciated Object for Structures Receiving (We admit that there is only one structure thanks to primary key constraint).
            if (ArticleRead != null)
                if (ArticleRead.Read())
                {
                    searchedArticle = new Article();  //_ Instanciation.

                    try  //_ Reference Filling.
                    {
                        searchedArticle.Reference = Int32.Parse(ArticleRead[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SelectByRef : " + ArticleRead.GetName(0) + " : ");
                        throw ex;
                    }

                    try //_ Designation Filling.
                    {
                        searchedArticle.Designation = ArticleRead[1].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SelectByRef : " + ArticleRead.GetName(1) + " : ");
                        throw ex;
                    }

                    try //_ Price Filling.
                    {
                        searchedArticle.Price = float.Parse(ArticleRead[2].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SelectByRef : " + ArticleRead.GetName(2) + " : ");
                        throw ex;
                    }
                
                }

            return searchedArticle;

        }
        public static List<Article> SelectAll()
        {
            OleDbCommand selectAllCommand = new OleDbCommand();   //_ Command Object.

            selectAllCommand.CommandType = System.Data.CommandType.TableDirect;   //_ Selection Query Configuration.
            selectAllCommand.CommandText = "Article";
            selectAllCommand.Connection = db;

            OleDbDataReader ArticlesRead = null; //_ Receiving Object.
            try     //_ Exécution de la requête.
            {
                ArticlesRead = selectAllCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            List<Article> allArticles = null;  //_ Local Collection of all structures.
            Article articleInter = null;

            if (ArticlesRead != null)
            {
                allArticles = new List<Article>();

                while (ArticlesRead.Read())  //_ Filling the Local Collection.
                {
                    articleInter = new Article();   //_ Article Instanciation.

                    //_ Artile Filling.
                    try //_ Reference Filling.
                    {
                        articleInter.Reference = Int32.Parse(ArticlesRead[0].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Select : " + ArticlesRead.GetName(0) + " : ");
                        throw ex;
                    }

                    try //_ Designation Filling.
                    {
                        articleInter.Designation = ArticlesRead[1].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Select : " + ArticlesRead.GetName(1) + " : ");
                        throw ex;
                    }

                    try //_ Price Filling.
                    {
                        articleInter.Price = float.Parse(ArticlesRead[2].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Select : " + ArticlesRead.GetName(2) + " : ");
                        throw ex;
                    }

                    //_ Article Adding.
                    try
                    {
                        allArticles.Add(articleInter);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                
                }
            
            }

            return allArticles;

        }

    }

}

