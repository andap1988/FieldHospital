using System;

namespace FieldHospital
{
    internal class Program
    {
        public static int Menu(int beds)
        {
            bool flag = true;
            string choice;
            int option = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("\n ......::::: UBS OneByAll :::::...... ");

                Console.WriteLine($"\n Atualmente temos um total de {beds} leito(s) disponiveis.\n");
                Console.WriteLine("\n ....::: Menu :::....\n");
                Console.WriteLine(" 1 - Gerar nova senha");
                Console.WriteLine(" 2 - Chamar uma pessoa para Triagem");
                Console.WriteLine(" 3 - Chamar uma pessoa para Exame");
                Console.WriteLine(" 4 - Area de Internacao");
                Console.WriteLine(" 5 - Cadastrar uma Emergencia");
                Console.WriteLine(" 6 - Recuperar Arquivos");
                Console.WriteLine(" 7 - Sair do Sistema\n");
                Console.Write(" Escolha: ");
                choice = Console.ReadLine();
                int.TryParse(choice, out option);

                if ((option < 1) || (option > 7))
                {
                    Console.WriteLine("\n xxxx Opcao invalida!\n");
                    Console.WriteLine(" xxxx Pressione ENTER para voltar ao menu...\n");
                    Console.ReadKey();
                }
                else
                {
                    flag = false;
                }
            } while (flag);

            return option;
        }

        public static int GenerateBeds()
        {
            string beds;
            int numBeds = 0;

            Console.WriteLine("\n Antes de iniciar o plantao, informe a quantidade de leitos que esta disponivel.\n");
            Console.Write(" Total disponivel: ");
            beds = Console.ReadLine();
            int.TryParse(beds, out numBeds);

            return numBeds;
        }

        static void Main(string[] args)
        {
            int numBeds = 0, option = 0;
            bool flag = true, flagUpload = true;

            Controller ControllerMaster = null;

            Console.WriteLine("\n ......::::: UBS OneByAll :::::...... ");
            numBeds = GenerateBeds();

            ControllerMaster = new Controller(numBeds);

            option = Menu(ControllerMaster.Hospitalization.Beds);

            do
            {
                switch (option)
                {
                    case 1:
                        Console.Clear();

                        ControllerMaster.NewPass();
                        option = Menu(ControllerMaster.Hospitalization.Beds);
                        break;
                    case 2:
                        Console.Clear();

                        ControllerMaster.Triage();
                        option = Menu(ControllerMaster.Hospitalization.Beds);
                        break;
                    case 3:
                        Console.Clear();

                        ControllerMaster.Exams();
                        option = Menu(ControllerMaster.Hospitalization.Beds);
                        break;
                    case 4:
                        Console.Clear();

                        ControllerMaster.Hospitalizations();
                        option = Menu(ControllerMaster.Hospitalization.Beds);
                        break;
                    case 5:
                        Console.Clear();

                        ControllerMaster.Emergency();
                        option = Menu(ControllerMaster.Hospitalization.Beds);
                        break;
                    case 6:
                        Console.Clear();

                        if (flagUpload)
                        {
                            flagUpload = false;
                            ControllerMaster.UploadData();
                        }
                        else
                        {
                            Console.WriteLine("\n xxxx Voce ja recuperou os arquivos. Nao e possivel faze-lo novamente.");
                            Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                            Console.ReadKey();
                        }

                        option = Menu(ControllerMaster.Hospitalization.Beds);
                        break;
                    case 7:
                        Console.Clear();
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\n xxxx Opcao invalida.\n");
                        Console.WriteLine(" xxxx Pressione ENTER para voltar...\n");
                        Console.ReadKey();
                        break;
                }
            } while (flag);
            Console.WriteLine("\n\n\t\t      ......::::: UBS OneByAll :::::...... ");
            Console.WriteLine("\n\n\t\t oooo Obrigado por utilizar nossos servicos oooo\n\n");
        }
    }
}
