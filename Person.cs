using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldHospital
{
    internal class Person
    { 
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string BirthDate { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Entry { get; set; }
        public string Exit { get; set; }
        public string Pass { get; set; }
        public string Temperature { get; set; }
        public string Saturation { get; set; }
        public string Comorbidities { get; set; }
        public string Symptoms { get; set; }
        public string DaysSymptoms { get; set; }
        public string ExamType { get; set; }
        public string ExamResult { get; set; }
        public string Hospitalization { get; set; }
        public Person Next { get; set; }


        public Person()
        {
            Name = null;
            Cpf = null;
            BirthDate = null;
            Age = null;
            Sex = null;
            Entry = null;
            Exit = "N/A";
            Pass = null;
            Temperature = null;
            Saturation = null;
            Comorbidities = "N/A";
            Symptoms = "N/A";
            DaysSymptoms = "N/A";
            ExamType = "N/A";
            ExamResult = "N/A";
            Hospitalization = "N/A";
            Next = null;
        }

        public override string ToString()
        {
            return "\n Dados do Paciente \n"
                + "\n Nome: " + Name
                + "\n CPF: " + Cpf
                + "\n Date de Nasc.: " + BirthDate
                + "\n Idade: " + Age
                + "\n Sexo: " + Sex
                + "\n Entrada: " + Entry
                + "\n Senha: " + Pass
                + "\n Temperatura: " + Temperature
                + "\n Saturacao: " + Saturation
                + "\n Comorbidades: " + Comorbidities
                + "\n Sintomas: " + Symptoms
                + "\n Dias Sintomas: " + DaysSymptoms
                + "\n Tipo Exame: " + ExamType
                + "\n Result Exame: " + ExamResult
                + "\n Internacao: " + Hospitalization
                +"\n";
        }

    }
}
