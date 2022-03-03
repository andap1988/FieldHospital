using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FieldHospital
{
    internal class Controller
    {
        public int Pass { get; set; }
        public int Next { get; set; }
        public PrincipalTriage PrincipalTriage { get; set; }
        public PreferenceTriage PreferenceTriage { get; set; }
        public Hospitalization Hospitalization { get; set; }
        public Exam Exam { get; set; }

        public Controller(int beds)
        {
            Hospitalization = new Hospitalization(beds);
            PrincipalTriage = new PrincipalTriage();
            PreferenceTriage = new PreferenceTriage();
            Exam = new Exam();
            Pass = 0;
            Next = 1;
        }

        public void NewPass()
        {
            string cpf, birthDate, age, pathPreference, pathPrincipal, dateNow, pass;
            DateTime dtNasc;
            int day, month, year, choice;
            bool flag = true;
            ConsoleKeyInfo key;
            Person tempPerson = null;

            pathPreference = @"C:\5by5-Texts\UBS\preferenceTriage\";
            pathPrincipal = @"C:\5by5-Texts\UBS\principalTriage\";

            do
            {
                Console.Clear();
                Console.WriteLine("\n ...:: Sistema Gerador de Senha ::...");
                Console.WriteLine(" Para geramos sua senha, nos informe os seguintes dados: \n");
                Console.Write(" CPF: ");
                cpf = Console.ReadLine();
                tempPerson = VerifyCpf(cpf);

                if (tempPerson != null)
                {
                    Console.WriteLine("\n Essa pessoa ja esta registrada em nosso sistema.");
                    Console.WriteLine(tempPerson.ToString());
                    Console.WriteLine(" Continuar com um novo atendimento? ");
                    Console.WriteLine(" 1 - SIM / 2 - NAO");
                    Console.Write("\n Escolha: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 2)
                    {
                        Console.WriteLine("\n xxxx Nao sera emitido uma nova senha e nao tera um novo atendimento.");
                        Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                        Console.ReadKey();
                        flag = false;
                    }
                }
                else if (tempPerson == null)
                {
                    Console.WriteLine(" Data de Nascimento: ");
                    Console.Write(" Dia: ");
                    day = Convert.ToInt32(Console.ReadLine());
                    Console.Write(" Mes: ");
                    month = Convert.ToInt32(Console.ReadLine());
                    Console.Write(" Ano: ");
                    year = Convert.ToInt32(Console.ReadLine());

                    dtNasc = new DateTime(year, month, day);
                    birthDate = dtNasc.ToString("dd/MM/yyyy");

                    age = Convert.ToString(Math.Floor(DateTime.Today.Subtract(dtNasc).TotalDays / 365));

                    Console.WriteLine($"\n Os dados informados foram: CPF: {cpf} e Idade: {age}");
                    Console.WriteLine("\n Se estiver correto, PRESSIONE ENTER. Caso esteja incorreto, PRESSIONE ESC para digitar novamente.");
                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Enter)
                    {
                        Pass++;
                        dateNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                        if (int.Parse(age) < 60) // principal triage
                        {
                            pass = "ppt" + Pass.ToString() + "-";
                            tempPerson = new Person();

                            tempPerson.Cpf = cpf;
                            tempPerson.BirthDate = birthDate;
                            tempPerson.Age = age;
                            tempPerson.Pass = pass;
                            tempPerson.Entry = dateNow;

                            PrincipalTriage.Push(tempPerson);

                            try
                            {
                                using (StreamWriter sw = new StreamWriter(pathPrincipal + pass + cpf + ".txt"))
                                {
                                    sw.WriteLine(cpf);
                                    sw.WriteLine(birthDate);
                                    sw.WriteLine(age);
                                    sw.WriteLine(pass);
                                    sw.WriteLine(dateNow);
                                }

                                Console.Clear();
                                Console.WriteLine("\t +--------- Fila Principal ---------+\n");
                                Console.WriteLine($"\t         A sua senha e: {pass}\n");
                                Console.WriteLine($"\t    Entrada as: {dateNow}\n");
                                Console.WriteLine("\t +----------------------------------+\n");
                                Console.WriteLine("\t   Aguarde na fila para ser chamado!");
                                Console.ReadKey();
                            }
                            catch
                            {
                                Console.WriteLine("\n\t xxxx Algo deu errado na gravacao.\n");
                                Console.WriteLine("\n\t xxxx Pressione ENTER para tentar novamente.\n");
                            }
                            flag = false;
                        }
                        else // preference triage
                        {
                            pass = "pft" + Pass.ToString() + "-";
                            tempPerson = new Person();

                            tempPerson.Cpf = cpf;
                            tempPerson.BirthDate = birthDate;
                            tempPerson.Age = age;
                            tempPerson.Pass = pass;
                            tempPerson.Entry = dateNow;

                            PreferenceTriage.Push(tempPerson);

                            try
                            {
                                using (StreamWriter sw = new StreamWriter(pathPreference + pass + cpf + ".txt"))
                                {
                                    sw.WriteLine(cpf);
                                    sw.WriteLine(birthDate);
                                    sw.WriteLine(age);
                                    sw.WriteLine(pass);
                                    sw.WriteLine(dateNow);
                                }

                                Console.Clear();
                                Console.WriteLine("\t +--------- Fila Preferencial ---------+\n");
                                Console.WriteLine($"\t          A sua senha e: {pass}\n");
                                Console.WriteLine($"\t     Entrada as: {dateNow}\n");
                                Console.WriteLine("\t +------------------------------------+\n");
                                Console.WriteLine("\t   Aguarde na fila para ser chamado!");
                                Console.ReadKey();
                            }
                            catch
                            {
                                Console.WriteLine("\n\t xxxx Algo deu errado na gravacao.\n");
                                Console.WriteLine("\n\t xxxx Pressione ENTER para tentar novamente.\n");
                            }
                            flag = false;
                        }
                    }
                }
            } while (flag);
        }

        public void NewTriage(Person inTriage)
        {
            int hasComorb = 0, hasSymp = 0;

            Console.WriteLine("\n CPF: " + inTriage.Cpf);
            Console.Write(" Nome: ");
            string name = Console.ReadLine();
            Console.Write(" Sexo (Masculino ou Feminino): ");
            string sex = Console.ReadLine();
            Console.Write(" Temperatura: ");
            string temperature = Console.ReadLine();
            Console.Write(" Saturacao: ");
            string saturation = Console.ReadLine();
            Console.Write(" Possui comorbidades (1 - SIM / 2 - NAO): ");
            string comorb = Console.ReadLine();
            int.TryParse(comorb, out hasComorb);

            if (hasComorb == 1)
            {
                Console.Write(" Quais?: ");
                comorb = Console.ReadLine();
            }
            else if (hasComorb == 2)
            {
                comorb = null;
            }

            Console.Write(" Possui sintomas (1 - SIM / 2 - NAO): ");
            string symptoms = Console.ReadLine();
            int.TryParse(symptoms, out hasSymp);
            string daysSymptoms = null;

            if (hasSymp == 1)
            {
                Console.Write(" Quantos dias faz que apareceram?: ");
                daysSymptoms = Console.ReadLine();
                Console.Write(" Quais?: ");
                symptoms = Console.ReadLine();
            }
            else if (hasSymp == 2)
            {
                symptoms = null;
                daysSymptoms = "0";
            }

            string dateNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            inTriage.Name = name;
            inTriage.Sex = sex;
            inTriage.Exit = dateNow;
            inTriage.Temperature = temperature;
            inTriage.Saturation = saturation;
            inTriage.Comorbidities = comorb;
            inTriage.Symptoms = symptoms;
            inTriage.DaysSymptoms = daysSymptoms;
            inTriage.ExamType = null;
            inTriage.ExamResult = null;
            inTriage.Hospitalization = null;

            if ((hasComorb == 2) && (hasSymp == 2) && (int.Parse(temperature) < 37) && (int.Parse(saturation) > 88)) // nao covid - fora 
            {
                RecordData(inTriage, "history");

                Console.WriteLine("\n Esse paciente nao possui indicativos de estar com COVID.");
                Console.WriteLine(" Os dados serão armazenados e o direcione para a ala comum.");
                Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                Console.ReadKey();
            }
            else
            {
                if ((hasComorb == 1) && (hasSymp == 1) && (double.Parse(temperature) > 36.7) && (int.Parse(saturation) < 90)) // possui todos - internacao
                {
                    RecordData(inTriage, "hosp");
                    Hospitalization.Push(inTriage);

                    Console.WriteLine("\n Esse paciente possui todos os indicativos de estar com COVID.");
                    Console.WriteLine(" Os dados serão armazenados e o mesmo sera encaminhado imediatamente para a internacao.");
                    Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
                else if ((int.Parse(inTriage.Age) > 60) && (hasComorb == 1) && (hasSymp == 1)) // mais de 60 com comorbidades e sintomas - internacao
                {
                    RecordData(inTriage, "hosp");
                    Hospitalization.Push(inTriage);

                    Console.WriteLine("\n Esse paciente possui os indicativos de estar com COVID.");
                    Console.WriteLine(" Os dados serão armazenados e o mesmo sera encaminhado para a internacao por estar no GRUPO DE RISCO.");
                    Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
                else if (((double.Parse(temperature) > 36.7) || (double.Parse(saturation) < 90)) && ((hasComorb == 1) || (hasSymp == 1))) // nao tem comorb nem sintomas, mas tem febre e/ou saturacao baixa - exame
                {
                    RecordData(inTriage, "exam");
                    Exam.Push(inTriage);

                    Console.WriteLine("\n Esse paciente possui indicativos de estar com COVID.");
                    Console.WriteLine(" Os dados serão armazenados e o mesmo sera encaminhado para o setor de exame.");
                    Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
                else if ((int.Parse(daysSymptoms) > 3) && (int.Parse(daysSymptoms) < 16)) // sintomas 4 ate 15 dias - exame
                {
                    RecordData(inTriage, "exam");
                    Exam.Push(inTriage);

                    Console.WriteLine("\n Esse paciente possui indicativos de estar com COVID.");
                    Console.WriteLine(" Os dados serão armazenados e o mesmo sera encaminhado para o setor de exame.");
                    Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
            }
        }

        public Person VerifyCpf(string cpf)
        {
            string pathHistory = @"C:\5by5-Texts\UBS\history\", cpfArchives = null;
            string[] data = new string[17];
            int i = -1;
            Person person = null;

            foreach (string file in Directory.GetFiles(pathHistory))
            {
                //cpfArchives = file.Split('-')[1];
                cpfArchives = file.ToString();
                Console.WriteLine("CPF ARCH -> " + cpfArchives);
                Console.ReadKey();
                if (cpfArchives == cpf)
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        do
                        {
                            i++;
                            data[i] = sr.ReadLine();
                        } while (data[i] != null);
                    }

                    person = new Person();

                    person.Name = data[0];
                    person.Cpf = data[1];
                    person.BirthDate = data[2];
                    person.Age = data[3];
                    person.Sex = data[4];
                    person.Entry = data[5];
                    person.Exit = data[6];
                    person.Pass = data[7];
                    person.Temperature = data[8];
                    person.Saturation = data[9];
                    person.Comorbidities = data[10];
                    person.Symptoms = data[11];
                    person.DaysSymptoms = data[12];
                    person.ExamType = data[13];
                    person.ExamResult = data[14];
                    person.Hospitalization = data[15];
                }
                else
                {
                    person = null;
                }
            }

            return person;
        }

        public void Triage()
        {
            int quantityPrefer = PreferenceTriage.Quantity(), quantityPrincip = PrincipalTriage.Quantity(), call = 0;
            bool flag = true;

            Person inTriage = null;

            do
            {
                if ((quantityPrefer < 1) && (quantityPrincip < 1))
                {
                    Console.Clear();
                    Console.WriteLine("\n xxxx Nao ha mais pessoas aguardando para triagem.");
                    Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                    Next = 0;
                    flag = false;
                }
                else if ((Next < 3) && (quantityPrefer > 0))
                {
                    Console.Clear();
                    Console.WriteLine("\n ...:: Iniciando a Triagem da Fila Preferencial ::... \n");
                    inTriage = PreferenceTriage.LoadPerson();
                    NewTriage(inTriage);
                    DeleteData(inTriage.Cpf, inTriage.Pass, "pref");

                    Next++;
                    flag = false;
                }
                else if ((Next < 3) && (quantityPrefer < 1))
                {
                    Console.Clear();
                    Console.WriteLine("\n xxxx Nao ha preferenciais aguardando.");
                    Console.WriteLine("\n xxxx Pressione ENTER para chamar uma pessoa da fila principal...");
                    Console.ReadKey();
                    Next = 3;
                }
                else if ((Next == 3) && (quantityPrincip > 0))
                {
                    Console.Clear();
                    Console.WriteLine("\n ...:: Iniciando a Triagem da Fila Principal ::...\n");
                    inTriage = PrincipalTriage.LoadPerson();
                    NewTriage(inTriage);
                    DeleteData(inTriage.Cpf, inTriage.Pass, "princ");

                    Next = 0;
                    flag = false;
                }
                else if ((Next == 3) && (quantityPrincip < 1))
                {
                    Console.Clear();
                    Console.WriteLine("\n xxxx Nao ha mais pessoas aguardando para triagem.");
                    Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                    Next = 0;
                    flag = false;
                }
            } while (flag);
        }

        public void Exams()
        {
            Person person = null;

            person = Exam.LoadPerson();

            if (person == null)
            {
                Console.WriteLine("\n xxxx Nao ha pessoas na fila de exame.");
                Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu....");
                Console.ReadKey();
            }
            else
            {
                int choiceExam = 0, resultExam = 0, state = 0;

                Console.Clear();
                Console.WriteLine("\n ...:: Area de Exames ::...");
                Console.WriteLine("\n Nome: " + person.Name);
                Console.WriteLine(" CPF: " + person.Cpf);
                Console.WriteLine("\n Qual exame o paciente ira fazer?");
                Console.WriteLine("\n 1 - PCR / 2 - Teste Rapido / 3 - Raio X");
                Console.Write("\n Escolha: ");
                choiceExam = Convert.ToInt32(Console.ReadLine());

                if (choiceExam == 1)
                {
                    Console.WriteLine(" +-----------------------------------+");
                    Console.WriteLine("\n Exame PCR\n");
                    Console.WriteLine("\n Qual foi o resultado do exame?");
                    Console.WriteLine("\n 1 - Positivo / 2 - Negativo");
                    Console.Write("\n Escolha: ");
                    resultExam = Convert.ToInt32(Console.ReadLine());
                    person.ExamType = "PCR";

                    if (resultExam == 1)
                    {
                        person.ExamResult = "Positivo";
                        Console.WriteLine("\n O paciente testou positivo para COVID-19.");
                        Console.WriteLine("\n Qual o atual estado do paciente?");
                        Console.WriteLine("\n 1 - Estado Grave / 2 - Estabilizado");
                        Console.Write("\n Escolha: ");
                        state = Convert.ToInt32(Console.ReadLine());

                        if (state == 1)
                        {
                            person.Hospitalization = "Internado";
                            Hospitalization.Push(person);
                            RecordData(person, "hosp");
                            DeleteData(person.Cpf, person.Pass, "exam");

                            Console.WriteLine("\n O paciente ira ser transferido para a internacao.");
                            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        }
                        else if (state == 2)
                        {
                            person.Hospitalization = "Em casa / Quarentena";

                            RecordData(person, "history");
                            DeleteData(person.Cpf, person.Pass, "exam");

                            Console.WriteLine("\n Informe o paciente que esta quarentena e passe os procedimentos para o mesmo");
                            Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        person.ExamResult = "Negativo";

                        RecordData(person, "history");
                        DeleteData(person.Cpf, person.Pass, "exam");

                        Console.WriteLine("\n O paciente testou negativo para COVID-19.");
                        Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                        Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        Console.ReadKey();
                    }

                }
                else if (choiceExam == 2)
                {
                    Console.WriteLine(" +-----------------------------------+");
                    Console.WriteLine("\n Teste Rapido\n");
                    Console.WriteLine("\n Qual foi o resultado do exame?");
                    Console.WriteLine("\n 1 - Positivo / 2 - Negativo");
                    Console.Write("\n Escolha: ");
                    resultExam = Convert.ToInt32(Console.ReadLine());
                    person.ExamType = "Teste Rapido";

                    if (resultExam == 1)
                    {
                        person.ExamResult = "Positivo";
                        Console.WriteLine("\n O paciente testou positivo para COVID-19.");
                        Console.WriteLine("\n Qual o atual estado do paciente?");
                        Console.WriteLine("\n 1 - Estado Grave / 2 - Estabilizado");
                        Console.Write("\n Escolha: ");
                        state = Convert.ToInt32(Console.ReadLine());

                        if (state == 1)
                        {
                            person.Hospitalization = "Internado";
                            Hospitalization.Push(person);
                            RecordData(person, "hosp");
                            DeleteData(person.Cpf, person.Pass, "exam");

                            Console.WriteLine("\n O paciente ira ser transferido para a internacao.");
                            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        }
                        else if (state == 2)
                        {
                            person.Hospitalization = "Em casa / Quarentena";

                            RecordData(person, "history");
                            DeleteData(person.Cpf, person.Pass, "exam");

                            Console.WriteLine("\n Informe o paciente que esta quarentena e passe os procedimentos para o mesmo");
                            Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        person.ExamResult = "Negativo";

                        RecordData(person, "history");
                        DeleteData(person.Cpf, person.Pass, "exam");

                        Console.WriteLine("\n O paciente testou negativo para COVID-19.");
                        Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                        Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        Console.ReadKey();
                    }
                }
                else if (choiceExam == 3)
                {
                    Console.WriteLine(" +-----------------------------------+");
                    Console.WriteLine("\n Raio X\n");
                    Console.WriteLine("\n Qual foi o resultado do exame?");
                    Console.WriteLine("\n 1 - Variacoes / 2 - Normal");
                    Console.Write("\n Escolha: ");
                    resultExam = Convert.ToInt32(Console.ReadLine());
                    person.ExamType = "Raio-X";

                    if (resultExam == 1)
                    {
                        person.ExamResult = "Variacoes";
                        Console.WriteLine("\n O Raio-X do paciente esta com variacoes.");
                        Console.WriteLine("\n Qual o atual estado do paciente?");
                        Console.WriteLine("\n 1 - Estado Grave / 2 - Estabilizado");
                        Console.Write("\n Escolha: ");
                        state = Convert.ToInt32(Console.ReadLine());

                        if (state == 1)
                        {
                            person.Hospitalization = "Internado";
                            Hospitalization.Push(person);
                            RecordData(person, "hosp");
                            DeleteData(person.Cpf, person.Pass, "exam");

                            Console.WriteLine("\n O paciente ira ser transferido para a internacao.");
                            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        }
                        else if (state == 2)
                        {
                            person.Hospitalization = "Em casa / Observacao";

                            RecordData(person, "history");
                            DeleteData(person.Cpf, person.Pass, "exam");

                            Console.WriteLine("\n Informe o paciente que esta em observacao e passe os procedimentos para o mesmo");
                            Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        person.ExamResult = "Normal";

                        RecordData(person, "history");
                        DeleteData(person.Cpf, person.Pass, "exam");

                        Console.WriteLine("\n O paciente nao possui variacoes.");
                        Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                        Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        Console.ReadKey();
                    }
                }
            }
        }

        public void Emergency()
        {
            int choice, hasComorb, hasSymp, choiceExam, day, month, year;
            string exam = null, resultExam = null, birthDate, age;
            DateTime dtNasc;

            Person person = null;

            Console.Clear();

            Console.WriteLine("\n ...:: Setor de Emergencia ::...\n");
            Console.WriteLine("\n Voce esta no setor de emergencia. Aqui os pacientes sao enviados diretamente para a internacao.");
            Console.WriteLine("\n Caso nao haja leitos disponiveis, o mesmo entrara na fila de espera de leitos.\n");

            Console.WriteLine("\n Solicitaremos os dados iniciais para registro.");
            Console.Write(" Nome: ");
            string name = Console.ReadLine();
            Console.Write(" CPF: ");
            string cpf = Console.ReadLine();

            person = new Person();

            Pass++;
            string dateNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            person.Name = name;
            person.Cpf = cpf;
            person.Entry = dateNow;
            person.Pass = "eme" + Pass.ToString() + "-";

            Hospitalization.Push(person);

            Console.WriteLine("\n Processo de internacao concluido!");
            Console.WriteLine("\n oooo Pressione ENTER para continuar o cadastro do paciente.");
            Console.ReadKey();

            Console.Clear();

            Console.WriteLine(" Nome: " + person.Name);
            Console.WriteLine(" CPF: " + person.Cpf);
            Console.WriteLine(" Data de Nascimento: ");
            Console.Write(" Dia: ");
            day = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Mes: ");
            month = Convert.ToInt32(Console.ReadLine());
            Console.Write(" Ano: ");
            year = Convert.ToInt32(Console.ReadLine());

            dtNasc = new DateTime(year, month, day);
            birthDate = dtNasc.ToString("dd/MM/yyyy");

            age = Convert.ToString(Math.Floor(DateTime.Today.Subtract(dtNasc).TotalDays / 365));

            Console.Write(" Sexo (Masculino ou Feminino): ");
            string sex = Console.ReadLine();
            Console.Write(" Temperatura: ");
            string temperature = Console.ReadLine();
            Console.Write(" Saturacao: ");
            string saturation = Console.ReadLine();
            Console.Write(" Possui comorbidades (1 - SIM / 2 - NAO): ");
            string comorb = Console.ReadLine();
            int.TryParse(comorb, out hasComorb);

            if (hasComorb == 1)
            {
                Console.Write(" Quais?: ");
                comorb = Console.ReadLine();
            }
            else if (hasComorb == 2)
            {
                comorb = null;
            }

            Console.Write(" Possui sintomas (1 - SIM / 2 - NAO): ");
            string symptoms = Console.ReadLine();
            int.TryParse(symptoms, out hasSymp);
            string daysSymptoms = null;

            if (hasSymp == 1)
            {
                Console.Write(" Quantos dias faz que apareceram?: ");
                daysSymptoms = Console.ReadLine();
                Console.Write(" Quais?: ");
                symptoms = Console.ReadLine();
            }
            else if (hasSymp == 2)
            {
                symptoms = null;
                daysSymptoms = "0";
            }

            Console.WriteLine(" Tipo de exame realizado?");
            Console.WriteLine("\n 1 - PCR / 2 - Teste Rapido / 3 - Raio X / 4 - NAO FEZ");
            Console.Write("\n Escolha: ");
            choiceExam = Convert.ToInt32(Console.ReadLine());

            if (choiceExam == 1)
                exam = "PCR";
            else if (choiceExam == 2)
                exam = "Teste Rapido";
            else if (choiceExam == 3)
                exam = "Raio-X";
            else if (choiceExam == 4)
                exam = "NAO FEZ";

            Console.WriteLine("\n Qual foi o resultado do exame?");
            Console.WriteLine("\n 1 - Positivo / 2 - Negativo / 3 - NAO FEZ");
            Console.Write("\n Escolha: ");
            choiceExam = Convert.ToInt32(Console.ReadLine());

            if (choiceExam == 1)
                resultExam = "Positivo";
            else if (choiceExam == 2)
                resultExam = "Negativo";
            else if (choiceExam == 3)
                resultExam = "NAO FEZ";

            person.BirthDate = birthDate;
            person.Age = age;
            person.Sex = sex;
            person.Exit = dateNow;
            person.Temperature = temperature;
            person.Saturation = saturation;
            person.Comorbidities = comorb;
            person.Symptoms = symptoms;
            person.DaysSymptoms = daysSymptoms;
            person.ExamType = exam;
            person.ExamResult = resultExam;
            person.Hospitalization = "Internado";

            RecordData(person, "hosp");

            Console.WriteLine("\n Todas as informacoes foram atualizadas.");
            Console.WriteLine(" oooo Pressione ENTER para voltar ao menu...");
            Console.ReadKey();
        }

        public void Hospitalizations()
        {
            Person person = null;

            int qtHosp = Hospitalization.QuantityBed(), choice = 0;

            //person = Hospitalization.LoadPerson();

            if (qtHosp < 1)
            {
                Console.WriteLine("\n xxxx Nao ha pessoas na internacao.");
                Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu....");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n ...:: Area de Internacao ::...");

                Console.WriteLine("\n Voce podera dar alta ou manter o paciente internado.");
                Console.WriteLine("\n +---------------------------------------------+\n");

                person = Hospitalization.HeadBed;

                for (int i = 0; i < qtHosp; i++)
                {
                    Console.WriteLine(person.ToString());

                    Console.WriteLine("\n Deseja dar alta para esse paciente?");
                    Console.WriteLine(" 1 - SIM / 2 - NAO");
                    Console.Write("\n Escolha: ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        Hospitalization.LoadBed(person);
                        person.Hospitalization = "Alta";
                        RecordData(person, "history");
                        DeleteData(person.Cpf, person.Pass, "hosp");
                        Console.WriteLine("\n Informe ao paciente que o mesmo teve alta hospitalar.");
                        Console.WriteLine(" Os dados serão armazenados e o direcione para a casa.");
                        Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
                        Console.ReadKey();

                        if (Hospitalization.QuantityLine() > 0)
                        {
                            person = Hospitalization.LoadPerson();
                            Hospitalization.Push(person);
                        }
                        break;
                    }
                    person = person.Next;
                }
            }
        }

        public void UploadData()
        {
            string pathExam = @"C:\5by5-Texts\UBS\exam\", pathHosp = @"C:\5by5-Texts\UBS\hospitalization\";
            string pathPreference = @"C:\5by5-Texts\UBS\preferenceTriage\", pathPrincipal = @"C:\5by5-Texts\UBS\principalTriage\";

            Person person = null;
            string[] data = new string[17];
            int i = -1;

            Console.WriteLine("\n ...:: Sistema de Recuperacao de Arquivos ::...");

            // recupera fila preferencial
            foreach (string file in Directory.GetFiles(pathPreference))
            {
                i = -1;
                using (StreamReader sr = new StreamReader(file))
                {
                    do
                    {
                        i++;
                        data[i] = sr.ReadLine();
                    } while (data[i] != null);
                }

                person = new Person();

                person.Cpf = data[0];
                person.BirthDate = data[1];
                person.Age = data[2];
                person.Pass = data[3];
                person.Entry = data[4];

                PreferenceTriage.Push(person);
            }

            // recupera fila principal
            foreach (string file in Directory.GetFiles(pathPrincipal))
            {
                i = -1;
                using (StreamReader sr = new StreamReader(file))
                {
                    do
                    {
                        i++;
                        data[i] = sr.ReadLine();
                    } while (data[i] != null);
                }

                person = new Person();

                person.Cpf = data[0];
                person.BirthDate = data[1];
                person.Age = data[2];
                person.Pass = data[3];
                person.Entry = data[4];

                PrincipalTriage.Push(person);
            }

            // recupera fila exame
            foreach (string file in Directory.GetFiles(pathExam))
            {
                i = -1;
                using (StreamReader sr = new StreamReader(file))
                {
                    do
                    {
                        i++;
                        data[i] = sr.ReadLine();
                    } while (data[i] != null);
                }

                person = new Person();

                person.Name = data[0];
                person.Cpf = data[1];
                person.BirthDate = data[2];
                person.Age = data[3];
                person.Sex = data[4];
                person.Entry = data[5];
                person.Exit = data[6];
                person.Pass = data[7];
                person.Temperature = data[8];
                person.Saturation = data[9];
                person.Comorbidities = data[10];
                person.Symptoms = data[11];
                person.DaysSymptoms = data[12];
                person.ExamType = data[13];
                person.ExamResult = data[14];
                person.Hospitalization = data[15];

                Exam.Push(person);
            }

            // recupera fila hospital
            foreach (string file in Directory.GetFiles(pathHosp))
            {
                i = -1;
                using (StreamReader sr = new StreamReader(file))
                {
                    do
                    {
                        i++;
                        data[i] = sr.ReadLine();
                    } while (data[i] != null);
                }

                person = new Person();

                person.Name = data[0];
                person.Cpf = data[1];
                person.BirthDate = data[2];
                person.Age = data[3];
                person.Sex = data[4];
                person.Entry = data[5];
                person.Exit = data[6];
                person.Pass = data[7];
                person.Temperature = data[8];
                person.Saturation = data[9];
                person.Comorbidities = data[10];
                person.Symptoms = data[11];
                person.DaysSymptoms = data[12];
                person.ExamType = data[13];
                person.ExamResult = data[14];
                person.Hospitalization = data[15];

                Hospitalization.Push(person);
            }

            Console.WriteLine("\n\n\n oooo Os arquivos foram recuperados com sucesso.\n");
            Console.WriteLine(" Todos os pacientes foram colocados em suas devidas filas.");
            Console.WriteLine("\n oooo Pressione ENTER para voltar ao menu...");
            Console.ReadKey();
        }

        public void DeleteData(string cpf, string pass, string sector)
        {
            string pathExam = @"C:\5by5-Texts\UBS\exam\", pathHosp = @"C:\5by5-Texts\UBS\hospitalization\", pathHistory = @"C:\5by5-Texts\UBS\history\";
            string pathPreference = @"C:\5by5-Texts\UBS\preferenceTriage\", pathPrincipal = @"C:\5by5-Texts\UBS\principalTriage\";

            string data = "";

            if (sector == "exam")
                data = pathExam + pass + cpf + ".txt";
            if (sector == "hosp")
                data = pathHosp + pass + cpf + ".txt";
            if (sector == "pref")
                data = pathPreference + pass + cpf + ".txt";
            if (sector == "princ")
                data = pathPrincipal + pass + cpf + ".txt";

            File.Delete(data);

        }

        public void RecordData(Person person, string sector)
        {
            string pathExam = @"C:\5by5-Texts\UBS\exam\", pathHosp = @"C:\5by5-Texts\UBS\hospitalization\", pathHistory = @"C:\5by5-Texts\UBS\history\";
            string pathPreference = @"C:\5by5-Texts\UBS\preferenceTriage\", pathPrincipal = @"C:\5by5-Texts\UBS\principalTriage\";

            if (sector == "history")
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(pathHistory + person.Pass + person.Cpf + ".txt"))
                    {
                        sw.WriteLine(person.Name);
                        sw.WriteLine(person.Cpf);
                        sw.WriteLine(person.BirthDate);
                        sw.WriteLine(person.Age);
                        sw.WriteLine(person.Sex);
                        sw.WriteLine(person.Entry);
                        sw.WriteLine(person.Exit);
                        sw.WriteLine(person.Pass);
                        sw.WriteLine(person.Temperature);
                        sw.WriteLine(person.Saturation);
                        sw.WriteLine(person.Comorbidities);
                        sw.WriteLine(person.Symptoms);
                        sw.WriteLine(person.DaysSymptoms);
                        sw.WriteLine(person.ExamType);
                        sw.WriteLine(person.ExamResult);
                        sw.WriteLine(person.Hospitalization);
                    }
                }
                catch
                {
                    Console.WriteLine("\n xxxx Algo deu errado na gravacao dos dados do paciente.");
                    Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
            }
            else if (sector == "hosp")
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(pathHosp + person.Pass + person.Cpf + ".txt"))
                    {
                        sw.WriteLine(person.Name);
                        sw.WriteLine(person.Cpf);
                        sw.WriteLine(person.BirthDate);
                        sw.WriteLine(person.Age);
                        sw.WriteLine(person.Sex);
                        sw.WriteLine(person.Entry);
                        sw.WriteLine(person.Exit);
                        sw.WriteLine(person.Pass);
                        sw.WriteLine(person.Temperature);
                        sw.WriteLine(person.Saturation);
                        sw.WriteLine(person.Comorbidities);
                        sw.WriteLine(person.Symptoms);
                        sw.WriteLine(person.DaysSymptoms);
                        sw.WriteLine(person.ExamType);
                        sw.WriteLine(person.ExamResult);
                        sw.WriteLine(person.Hospitalization);
                    }
                }
                catch
                {
                    Console.WriteLine("\n xxxx Algo deu errado na gravacao dos dados do paciente.");
                    Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
            }
            else if (sector == "exam")
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(pathExam + person.Pass + person.Cpf + ".txt"))
                    {
                        sw.WriteLine(person.Name);
                        sw.WriteLine(person.Cpf);
                        sw.WriteLine(person.BirthDate);
                        sw.WriteLine(person.Age);
                        sw.WriteLine(person.Sex);
                        sw.WriteLine(person.Entry);
                        sw.WriteLine(person.Exit);
                        sw.WriteLine(person.Pass);
                        sw.WriteLine(person.Temperature);
                        sw.WriteLine(person.Saturation);
                        sw.WriteLine(person.Comorbidities);
                        sw.WriteLine(person.Symptoms);
                        sw.WriteLine(person.DaysSymptoms);
                        sw.WriteLine(person.ExamType);
                        sw.WriteLine(person.ExamResult);
                        sw.WriteLine(person.Hospitalization);
                    }
                }
                catch
                {
                    Console.WriteLine("\n xxxx Algo deu errado na gravacao dos dados do paciente.");
                    Console.WriteLine("\n xxxx Pressione ENTER para voltar ao menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
