using System;
using System.Collections.Generic;
using System.Globalization;


namespace DataBase_Interface
{
    public class ArticleView
    {
        /*_ PRINT _*/
        public static void DevelopperInfo()   //_ Developer Informations.
        {
            Console.WriteLine("/////////////////////////////////////////////////////////////");
            Console.WriteLine("/// BY //////////////////////////////////////////////////////");
            Console.WriteLine("/// MARRAKCHI Ghassen ///////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////");
            Console.WriteLine("/// DB CONNECTION ///////////////////////////////////////////");
            Console.WriteLine("/// SYNCHRONUS MODE//////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////");
            Console.WriteLine("/// ADO.NET /////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////");
            Console.WriteLine("/// CONSOLE MODE ////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////");
            Console.WriteLine("/// VERSION /////////////////////////////////////////////////");
            Console.WriteLine("/// BETA ////////////////////////////////////////////////////");
            Console.WriteLine("/////////////////////////////////////////////////////////////");

        }
        public static string Setting()   //_ DataBase File Settings.
        {
            string nomFichierBase;

            Console.WriteLine("/////////////////////////////////////////////////// DATABASE SETTING //");
            Console.WriteLine("// Enter the DataBase File Name (Absolute Path) ///////////////////////");
            nomFichierBase = Console.ReadLine();
            Console.WriteLine("///////////////////////////////////////////////////////////////////////");

            return nomFichierBase;

        }
        public static void Menu()   //_ Navigation Menu for users.
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("1- Show All Articles ------------");
            Console.WriteLine("2- Add New Article --------------");
            Console.WriteLine("3- Search Article ---------------");
            Console.WriteLine("4- Modify Article ---------------");
            Console.WriteLine("5- Delete Article ---------------");
            Console.WriteLine("0- Quit -------------------------");
            Console.WriteLine("---------------------------------");

        }
        public static void LinesJump()   //_ Formatage d'Affichage.
        {
            for (int i = 0; i < 50; i++)
                Console.WriteLine();

        }


        /*_ COMMANDES _*/
        public static void PrintAllCommand()   //_ PRINT All Articles.
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Articles ------------------------");
            Console.WriteLine("---------------------------------");

            List<Article> printing = null;
            try //_ Articles Receiving.
            {
                printing = ArticleDAL.SelectAll();
                Console.WriteLine("Mapping Done ..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Show(printing);    //_ Affichage.

        }
        public static void AddCommand()        //_ ADDING Article.
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("ADDING Article ---------------");
            Console.WriteLine("------------------------------");

            Console.WriteLine("Enter Article Data : ");
            Article articleAdd = GetArticleFromUser();    //_ Instanciation of the Article filled by the user.

            try
            {
                ArticleDAL.Add(articleAdd);   //_ Adding the Article.
                Console.WriteLine("Article Added Successfully ..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void SearchByRefCommand()   //_ SEARCHING Article.
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("SEARCHING Article ------------");
            Console.WriteLine("------------------------------");


            int Ref = 0;    //_ Article Reference.


            bool correct = false;   //_ Reference Filling.
            do
            {   //_ Entry Type Verification.
                Console.Write("Enter the Article Reference : _");
                try
                {
                    Ref = Int32.Parse(Console.ReadLine());
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (correct == false);


            Article searchedArticle = null;   //_ Searching for the Article.
            try
            {
                searchedArticle = ArticleDAL.SelectByRef(Ref);
                Console.WriteLine("Searching Done Successfully ..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (searchedArticle != null)  //_ null : Article Not Found.
                Show(searchedArticle);
            else
                Console.WriteLine("Article Not Found.");

        }
        public static void ModificationCommand()       //_ MODIFICATION of Article.
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("MODIFICATION of Article ------");
            Console.WriteLine("------------------------------");


            int Ref = 0;    //_ Article Reference.


            bool correct = false;   //_ Reference Filling.
            do
            {   //_ Type Verification.
                Console.Write("Enter the Article Reference : _");
                try
                {
                    Ref = Int32.Parse(Console.ReadLine());
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (correct == false);


            Article searchedArticle = null; //_ Searching for the Article.
            try
            {
                searchedArticle = ArticleDAL.SelectByRef(Ref);
                Console.WriteLine("Recherche Effectuée ..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (searchedArticle != null)  //_ null : Article introuvable.
            {
                Show(searchedArticle);    //_ Affichage de l'Article Trouvé.
                Console.WriteLine();

                //_ Saisie des informations manquantes.
                Article updatedArticle = new Article(Ref, "", 0);

                Console.Write("--Designation : _"); //_ Designation Filling.
                updatedArticle.Designation = Console.ReadLine();

                correct = false;    //_ Price Filling.
                do
                {   //_ Type Verification.
                    Console.Write("--Prce : _");
                    try
                    {
                        updatedArticle.Price = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        correct = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (correct == false);


                Show(updatedArticle);    //_ Modification Printing.
                Console.WriteLine();

                correct = false;    //_ Confirmation of the Modifications.
                char choice = 'n';
                do //_ Type Verification.
                {
                    Console.Write("\nDo you Confirm the modifications ? [(Y): Yes / (n): no] _");
                    try
                    {
                        choice = char.Parse(Console.ReadLine());
                        correct = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (correct == false);


                if (choice == 'Y')   //_ Verification de la confirmation.
                {
                    //_ Mise à jour.
                    try
                    {
                        ArticleDAL.Update(Ref, updatedArticle);
                        Console.WriteLine("Article Modified Successfully ..");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("No Modification Added ..");

            }
            else
                Console.WriteLine("Article Not Found");

        }
        public static void DeleteCommand()      //_ SUPRESSION d'un Article.
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("DELETING Article -------------");
            Console.WriteLine("------------------------------");


            int Ref = 0;    //_ Reference.


            //_ Reference Filling.
            bool correct = false;
            do
            {
                Console.Write("Enter the Article Reference : _");
                try //_ Type Verification.
                {
                    Ref = Int32.Parse(Console.ReadLine());
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (correct == false);


            Article searchedArticle = null;   //_ Searching.
            try
            {
                searchedArticle = ArticleDAL.SelectByRef(Ref);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (searchedArticle != null)
            {
                Show(searchedArticle);    //_ Search Result Printing.


                //_ Deleting Confirmation.
                char choice = 'n';
                correct = false;
                do  //_ Type Verification.
                {
                    Console.Write("\nDo you Confirm Deleting ? [(Y): Yes / (n): no] _");
                    try
                    {
                        choice = char.Parse(Console.ReadLine());
                        correct = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (correct == false);


                if (choice == 'Y')   //_ Deleting Confirmed.
                {
                    try
                    {
                        ArticleDAL.Delete(Ref);
                        Console.WriteLine("Article Deleted Successfully ..");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    Console.WriteLine("No Article Deleting..");
            }
            else
                Console.WriteLine("Article Not Found");

        }


        /*_ UTILITAIRES _*/
        public static void Show(Article a)  //_ PRINTING an Article.
        {
            if (a != null)
                Console.Write("|{0,-13}|{1,-29}|{2,-16}|", a.Reference, a.Designation, a.Price);
            else
                Console.WriteLine("Article Not Defined!");

        }
        public static void Show(List<Article> Articles)  //_ PRINTING an Article Collection.
        {
            if (Articles != null)   //_ Empty Collection.
            {
                if (Articles.Count != 0)
                {
                    //_ Header.
                    Console.WriteLine(" _____________ _____________________________ ________________ ");
                    Console.WriteLine("|  Reference  |         Designation         |      Price     |");

                    //_ Elements.
                    int index = 0;
                    while (index < Articles.Count)
                    {
                        Console.WriteLine(" ____________ _____________________________ ________________ ");
                        Show(Articles[index]);
                        Console.WriteLine();
                        index++;
                    }

                    //_ Buttom Sepration.
                    Console.WriteLine(" ____________ _____________________________ ________________ ");
                }
                else
                    Console.WriteLine("No Article Found!");
            }
            else
                Console.WriteLine("No Article Found!");

        }

        public static Article GetArticleFromUser()  //_ User Entered Article Instanciation.
        {
            Article articleUser = new Article();    //_ Entry Object.


            //_ Filling.
            bool correct = false;
            do  //_ Reference.
            {
                Console.Write("--Reference : _");

                try //_ Type Verification. 
                {
                    articleUser.Reference = Int32.Parse(Console.ReadLine());
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (correct == false);

            Console.Write("--Designation : _");     //_ Designation.
            articleUser.Designation = Console.ReadLine();

            //_ Prix.
            correct = false;
            do
            {
                Console.Write("--Price : _");

                try //_ Type Verification.
                {
                    articleUser.Price = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);    //_ Float Conversion.
                    correct = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (correct == false);


            return articleUser; //_ Entry Return.

        }

    }
}
