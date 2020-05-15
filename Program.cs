/*
 * 
 * DataBase Interface.
 * * Simple DataBase Composed by a unique Table "Article"
 * * Connection in Synchronus Mode.
 * 
 * Article Table
 * * Reference   : Numeric | PRIMARY KEY.
 * * Designation : Small Text.
 * * Price       : Simple Real (Float).
 * 
 * DataBase Engine
 * * MS. ACCESS
 * 
 * Technology
 * * ADO.NET (OleDb API)
 * 
 * Printing Mode
 * * Console.
 * 
 * BY
 * *MARRAKCHI Ghassen
 * 
 */

using System;

namespace DataBase_Interface
{
    class Program
    {
        static void Main()
        {
            /*_ INFORMATIONS _*/
            ArticleView.LinesJump();
            ArticleView.DevelopperInfo();
            Console.ReadLine();


            /*_ DB_CONNECTION _*/
            ArticleView.LinesJump();

            string FichierBD = ArticleView.Setting();
            try
            {
                ArticleDAL.ConnectTo(FichierBD);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto PROGRAM_END;
            }
            Console.ReadLine();


            /*_ MAIN_PROGRAM _*/
            int choix = -1;
            while (choix != 0)
            {
                ArticleView.LinesJump();
                ArticleView.Menu();

                /*_ DECISION_ENTRY _*/
                bool correct = false;
                do
                {   //_ Format Verification.
                    Console.Write("Enter your choice : _");
                    try
                    {
                        choix = Int32.Parse(Console.ReadLine());
                        correct = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (correct == false);


                /*_ REDIRECTION _*/
                switch (choix)
                {
                    case 1: //_ PRINT.
                        ArticleView.LinesJump();
                        ArticleView.PrintAllCommand();
                        Console.ReadLine();
                        break;

                    case 2: //_ ADD.
                        ArticleView.LinesJump();
                        ArticleView.AddCommand();
                        Console.ReadLine();
                        break;

                    case 3: //_ SEARCH.
                        ArticleView.LinesJump();
                        ArticleView.SearchByRefCommand();
                        Console.ReadLine();
                        break;

                    case 4: //_ MODIFICATION.
                        ArticleView.LinesJump();
                        ArticleView.ModificationCommand();
                        Console.ReadLine();
                        break;

                    case 5: //_ DELETE.
                        ArticleView.LinesJump();
                        ArticleView.DeleteCommand();
                        Console.ReadLine();
                        break;

                    case 0: //_ QUIT.
                        goto DISCONNECTION;

                    default:    //_ ERROR.
                        Console.WriteLine("WRONG CHOICE");
                        Console.ReadLine();
                        break;
                }
            }


        /*_ DB_DISCONNECTION _*/
        DISCONNECTION: 
            ArticleView.LinesJump();
            try
            {
                ArticleDAL.DisconnectFromDB();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto PROGRAM_END;
            }


            /*_ QUIT _*/
            Console.WriteLine("Thank you for visiting !");
            ArticleView.DevelopperInfo();


        PROGRAM_END:;
        }
    }
}
