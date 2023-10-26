using System;
using System.Collections.Generic;
using System.Linq;

namespace UD2T1AguilarAlba.Tarea1 {
    public class Empresa {
        private List<Empleado> ListaEmpleados = new List<Empleado>();
        private Pedirdatos ped = new Pedirdatos();
        // private String direccion = "Empresa.txt";
        public Empresa() {
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
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif();
                nif = ped.PedirStringSinControl();
                if ( nif.Length > 0 ) {
                    if ( tipo ) {
                        Console.Write( ListaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista(  ) ) ).Edad );
                    } else {
                        ListaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista(  ) ) ).Edad = ped.PedirIntPositivo( "Nueva edad que quieras introducir" );
                        Console.Write( "Cambio de edad realizado" );
                    }
                } else {
                    Console.Write( "Saliendo...\n" );
                }
            }
        }
        private void AccederNombre(  bool tipo ) { // true: ver nombre, false: modificar nombre
            string nif;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif(  );
                if ( ( nif = ped.PedirStringSinControl() ).Length > 0 ) {
                    if ( ExisteNif(  nif ) ) {
                        if ( tipo ) {
                            Console.Write( DevolverEmpleado( nif ).Nombre );
                        } else {
                            DevolverEmpleado(  nif ).Nombre = ped.PedirString( "Que nombre quieres introducir al nuevo usuario" );
                            Console.Write( "Cambio de nombre realizado" );
                        }
                    }
                } else {
                    Console.Write( "Saliendo...\n" );
                }
            }
        }
        private void ActualizarSalario(  ) {
            string nif;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( );
                if ( ( nif = ped.PedirStringSinControl() ).Length > 0 ) {
                    if ( ExisteNif(  nif ) ) {
                        DevolverEmpleado( nif ).Salario = ped.PedirDoublePositivo( "Que salario quieres introducir al nuevo usuario" );
                        Console.Write( "Cambio de nombre realizado" );
                    } else {
                        Console.Write( "No existe el usuario" );
                    }
                } else {
                    Console.Write( "Saliendo...\n" );
                }
            }
        }
        private Empleado DevolverEmpleado( string nif ) {
            return ListaEmpleados.Find( ( obj ) => obj.Nif.Equals( nif ) );
        }
        private void EliminarEmpleado( List<Empleado> listaEmpleados ) {
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( );
                listaEmpleados.RemoveAll( ( obj ) => obj.Equals( PedirNifLista( ) ) );
                Console.Write( "Fue borrado con exito" );
            }
        }
        private string PedirNifLista(  ) {
            if ( ListaEmpleados.Count == 0 ) {
                throw new ArgumentException();
            }
            string nif;
            bool salida = false;
            do {
                if ( ExisteNif( ( nif = ped.PedirString( "Introduce el nif" ) ) ) ) {
                    Console.Write( "Ese nif no esta en la lista" );
                } else {
                    salida = true;
                }
            } while ( !salida );
            return nif;
        }
        private void ComoMostrarEmpleado( ) {
            string nif;
            if ( ListaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( );
                Console.Write( "Si introduces un blanco retornas al menu" );
                if ( ( nif = ped.PedirStringSinControl( "Pasame el nif que quieras buscar" ) ).Length > 0 ) {
                    MostrarUnEmpleado(  nif );
                } else {
                    MostrarTodosEmpleados( ListaEmpleados );
                }
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
