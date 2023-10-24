using System;
namespace UD2T1AguilarAlba.Tarea1 {
    public class Empleado {

        private string nombre;

        private string apellido1;
        private string apellido2;
        private int edad;
        private string nif;
        private double salario;

        public Empleado(string nombre, string apellido1 , string apellido2, int edad, string nif , double salario ) {
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.edad = edad;
            this.nif = nif;
            this.salario = salario;
        }
        public string Nombre {
            get {
                return nombre;
            }
        }

        public string Apellido1 {
            get {
                return apellido1;
            }
        }

        public  string Apellido2 {
            get {
                return apellido2;
            }
        }

        public int Edad {
            get {
                return edad;
            }
        }

        public  string Nif {
            get {
                return nif;
            }
        }

        public double Salario {
            get {
                return salario;
            }
        }
        public String MostrarEmpleado() {
            return string.Format( "Nombre: {0} {1} {2}\nEdad: {3}\nNIF: {4}\nSalario: {5}€",nombre,apellido1,apellido2,edad,nif,salario );
        }

        public String StringEmpleado() {
            return string.Format( "{0}/{1}/{2}/{3}/{4}/{5}",nif, nombre, apellido1, apellido2, edad, salario );
        }
    }
}
