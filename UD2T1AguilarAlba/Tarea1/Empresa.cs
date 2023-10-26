using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UD2T1AguilarAlba.Tarea1 {
    public class Empresa {
        private List<Empleado> ListaEmpleados = new List<Empleado>();
        private Pedirdatos ped = new Pedirdatos();
        private string Direccion = "ArchivoEmpresaEmpleado.cs";

        public Empresa() {
            LeerEmpeladosFichero();
        }
        public void EscribrirEmpleadosFichero() {
            StreamWriter escritor = new StreamWriter( Direccion );
            if ( ListaEmpleados.Count>0) {
                foreach (Empleado persona in ListaEmpleados ) {
                        escritor.WriteLine("//"+persona.StringEmpleado() );
                   }
            } else {
                escritor.Write( "" );
            }
            escritor.Close();
        }
        private void LeerEmpeladosFichero() {
            StreamReader lector= null;
            string contenido;
            try {
                lector = new StreamReader( Direccion );
                contenido = lector.ReadLine();
                while ( contenido != null ) {

                    ListaEmpleados.Add( DeStringAEmpleado (contenido ));
                    contenido = lector.ReadLine();
                }
            } catch ( Exception ) {

            } finally {
                if ( lector != null ) {
                    lector.Close();
                }
                Console.WriteLine( "Reconstrución de datos" );
            }

        }
        private Empleado DeStringAEmpleado(string empladoString) {
            string[] listaAtributos= empladoString.Split('/');
            return new Empleado( listaAtributos [3], listaAtributos [4], listaAtributos [5],Int16.Parse( listaAtributos[6]), listaAtributos[2],Double.Parse( listaAtributos[7] ));
        }
        public void CrearEmpleado() {
            bool salida = false;
            int edad;
            string nombre, apellido1, apellido2, nif;
            double salario;
            Console.Write( "Has entrado en el programa de creación de personal\n Introduce el nif del usuario" );
            do {
                nif = ped.PedirString();
                if ( ListaEmpleados.Count == 0 || ListaEmpleados.Find( ( Empleado obj ) => obj.Nif.Equals( nif ) ) == null ) {
                    salida = true;
                } else {
                    Console.Write( "Ya esta el usuario inscrito" );
                }
            } while ( !salida );
            nombre = ped.PedirString( "Introduce el nombre del usuario" );
            apellido1 = ped.PedirString( "Introduce el primer apellido del usuario" );
            apellido2 = ped.PedirString( "Introduce el segundo apellido del usuario" );
            edad = ped.PedirIntPositivo( "Introduce la edad del usuario" );
            salario = ped.PedirDoublePositivo( "Introduce el salario del individio" );
            ListaEmpleados.Add( new Empleado( nombre, apellido1, apellido2, edad, nif, salario ) );
            Console.Write( "El empleado ha sido creado" );
        }
        private void MostrarTodosEmpleados( List<Empleado> listaEmpleados ) {
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                foreach ( Empleado empleado in listaEmpleados ) {
                    Console.Write( empleado.MostrarEmpleado() );

                }
            }
        }

        public void AccederEdad( bool tipo ) { // true: ver Edad, false: modificar edad
            string nif;
            bool salida = false;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif();
                do {
                    if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                        if ( ExisteNif( nif ) ) {
                            if ( tipo ) {
                                Console.Write( "Su edad es -> " );
                                Console.Write( ListaEmpleados.Find( ( obj ) => obj.Nif.Equals( nif ) ).Edad );
                            } else {
                                ListaEmpleados.Find( ( obj ) => obj.Nif.Equals( nif ) ).Edad = ped.PedirIntPositivo( "Nueva edad que quieras introducir" );
                                Console.Write( "Cambio de edad realizado" );
                            }
                            salida = true;
                        } else {
                            Console.Write( "No existe el usuario" );
                        }
                    } else {
                        salida = true;
                        Console.Write( "Saliendo...\n" );
                    }
                } while (!salida);
            }
        }
        public void AccederNombre(  bool tipo ) { // true: ver nombre, false: modificar nombre
            string nif;
            bool salida=false;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif(  );
                do {
                    if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                        if ( ExisteNif( nif ) ) {
                            if ( tipo ) {
                                Console.Write( "Su nombres es -> " );
                                Console.Write( DevolverEmpleado( nif ).Nombre );
                            } else {
                                DevolverEmpleado( nif ).Nombre = ped.PedirString( "Que nombre quieres introducir al nuevo usuario" );
                                Console.Write( "Cambio de nombre realizado" );
                            }
                            salida = true;
                        } else {
                            Console.Write( "No existe el usuario" );
                        }
                    } else {
                        salida = true;
                        Console.Write( "Saliendo...\n" );
                    }
                } while (!salida );
            }
        }
        public void ActualizarSalario(  ) {
            string nif;
            bool salida=false;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( );
                do {
                    if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                        if ( ExisteNif( nif ) ) {
                            DevolverEmpleado( nif ).Salario = ped.PedirDoublePositivo( "Que salario quieres introducir al nuevo usuario" );
                            Console.Write( "Cambio de nombre realizado" );
                            salida = true;
                        } else {
                            Console.Write( "No existe el usuario" );
                        }
                    } else {
                        salida = true;
                        Console.Write( "Saliendo...\n" );
                    }
                } while ( !salida );
            }
        }
        private Empleado DevolverEmpleado( string nif ) {
            return ListaEmpleados.Find( ( obj ) => obj.Nif.Equals( nif ) );
        }
        public void EliminarEmpleado( ) {
            string nif;
            bool salida=false;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( );
                do {
                    if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                        if ( ExisteNif( nif ) ) {
                            ListaEmpleados.RemoveAll( ( obj ) => obj.Equals( nif ) );
                            Console.Write( "Fue borrado con exito" );
                            salida = true;
                        } else {
                            Console.Write( "No existe el usuario\n" );
                        }
                    } else {
                        salida = true;
                        Console.Write( "Saliendo...\n" );
                    }
                } while (!salida );
            }
        }

        public void ComoMostrarEmpleado( ) {
            string nif;
            bool salida = false;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif();
                Console.Write( "Si introduces un blanco retornas al menu" );
                do { 
                if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                    if ( ExisteNif( nif ) ) {
                        MostrarUnEmpleado( nif );
                        salida = true;
                    } else {
                        Console.Write( "No existe el usuario" );
                    }
                } else {
                    MostrarTodosEmpleados( ListaEmpleados );
                    salida = true;
                }
            }while (!salida ) ;
            }
        }
        private bool ExisteNif(  string nif ) {// true: si esta false: no esta
            return ListaEmpleados.Find( ( arg ) => arg.Nif.Equals( nif ) ) != null;
        }
        private void MostrarUnEmpleado(  String nif ) {
            Empleado empleado = ListaEmpleados.Find( ( Empleado arg ) => arg.Nif.Equals( nif ) );
            if ( empleado == null ) {
                Console.Write( "No esta el empleado con el nif " + nif );
            } else {
                Console.Write( empleado.MostrarEmpleado() );
            }
        }
        private void MostrarListaNif() {
            Console.Write( "Los nif dispibles son (" + string.Join( "-", ListaEmpleados.Select( t => t.Nif ) ) + ")\n" );
        }

    }

   
}
