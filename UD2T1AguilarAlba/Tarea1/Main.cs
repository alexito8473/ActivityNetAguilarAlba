using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
///////////////////////////////////////////
// Tarea: UD2T1
// Alumno/a: Alejandro Aguilar Alba
// Curso: 2023/2024
///////////////////////////////////////////
namespace UD2T1AguilarAlba.Tarea1 {
    class MainClass {

        private const int MAXIMO = 8;
        private const int SALIDA = 0;
        private const int CREAR_EMPLEADO = 1;
        private const int ACTUALIZAR_SALARIO = 2;
        private const int MOSTRAR_NOMBRE = 3;
        private const int ACTUALIZAR_NOMBRE = 4;
        private const int MOSTRAR_EDAD = 5;
        private const int MODIFCAR_EDAD = 6;
        private const int ELIMINAR_EMPLEADO = 7;
        private const int MOSTRAR_EMPLEADO = 8;

        private Pedirdatos ped = new Pedirdatos();

        private void Tarea1() {
            List<Empleado> listaEmpleados = new List<Empleado>();
            Menu( listaEmpleados );
        }
        private void Menu( List<Empleado> listaEmpleados ) {
            bool salida = false;
            do {
                Console.Write( "\n---Menu---\n0 -> Salir\n1 -> Crear empleado\n2 -> Actualizar salario\n3 -> Mostrar nombre\n4 -> Actualizar nombre\n5 -> Mostrar edad\n6 -> Modificar edad\n7 -> Eliminar empleado\n8 -> Mostrar empleado/Mostrar todos\n" );
                switch ( ped.PedirIntEnRango( 0, MAXIMO ) ) {
                    case SALIDA:
                        salida = true;
                        break;
                    case CREAR_EMPLEADO:
                        CrearEmpleado( listaEmpleados );
                        break;
                    case ACTUALIZAR_SALARIO:
                        ActualizarSalario( listaEmpleados );
                        break;
                    case MOSTRAR_NOMBRE:
                        AccederNombre( listaEmpleados, true );
                        break;
                    case ACTUALIZAR_NOMBRE:
                        AccederNombre( listaEmpleados, false );
                        break;
                    case MOSTRAR_EDAD:
                        AccederEdad( listaEmpleados, true );
                        break;
                    case MODIFCAR_EDAD:
                        AccederEdad( listaEmpleados, false );
                        break;
                    case ELIMINAR_EMPLEADO:
                        EliminarEmpleado( listaEmpleados );
                        break;
                    case MOSTRAR_EMPLEADO:
                        ComoMostrarEmpleado( listaEmpleados );
                        break;
                }
            } while ( !salida );
            Console.Write( "Se cerro el programa" );
        }
        private void AccederEdad( List<Empleado> listaEmpleados, bool tipo ) { // true: ver Edad, false: modificar edad
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( listaEmpleados );
                if ( tipo ) {
                    Console.Write( listaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista( listaEmpleados ) ) ).Edad );
                } else {
                    listaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista( listaEmpleados ) ) ).Edad = ped.PedirIntPositivo( "Nueva edad que quieras introducir" );
                    Console.Write( "Cambio de edad realizado" );
                }
            }
        }

        private void AccederNombre( List<Empleado> listaEmpleados, bool tipo ) { // true: ver nombre, false: modificar nombre
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( listaEmpleados );
                if ( tipo ) {
                    Console.Write( listaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista( listaEmpleados ) ) ).Nombre );
                } else {
                    listaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista( listaEmpleados ) ) ).Nombre = ped.PedirString( "Que nombre quieres introducir al nuevo usuario" );
                    Console.Write( "Cambio de nombre realizado" );
                }
            }
        }

        private void ActualizarSalario( List<Empleado> listaEmpleados ) {
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( listaEmpleados );
                listaEmpleados.Find( ( obj ) => obj.Nif.Equals( PedirNifLista( listaEmpleados ) ) ).Salario = ped.PedirDoublePositivo( "Que salario quieres introducir al nuevo usuario" );
                Console.Write( "Cambio de nombre realizado" );
            }
        }

        private void MostrarListaNif( List<Empleado> listaEmpleados ) {
            Console.Write( "Los nif dispibles son (" + string.Join( "-", listaEmpleados.Select( t => t.Nif ) ) + ")\n" );
        }
        private void ComoMostrarEmpleado( List<Empleado> listaEmpleados ) {
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                Console.Write( "Quires mostrar solo un usuario, pero poniendome tu el nif(uno) o motrar todos los datos(todo)" );
                if ( ped.Pedirbool( "uno", "todo" ) ) {
                    MostrarListaNif( listaEmpleados );
                    Console.Write( "Pasame el nif que quieras buscar" );
                    MostrarUnEmpleado( listaEmpleados, ped.PedirString() );
                } else {
                    MostrarTodosEmpleados( listaEmpleados );
                }
            }

        }

        private void EliminarEmpleado( List<Empleado> listaEmpleados ) {
            if ( listaEmpleados.Count == 0 ) {
                Console.Write( "No hay empleados" );
            } else {
                MostrarListaNif( listaEmpleados );
                listaEmpleados.RemoveAll( ( obj ) => obj.Equals( PedirNifLista( listaEmpleados ) ) );
                Console.Write( "Fue borrado con exito" );
            }
        }

        private String PedirNifLista( List<Empleado> listaEmpleados ) {
            if ( listaEmpleados.Count == 0 ) {
                throw new ArgumentException();
            }
            string nif;
            bool salida = false;
            Console.Write( "Introduce el nif" );
            do {
                nif = ped.PedirString();
                if ( listaEmpleados.Find( ( Empleado arg ) => arg.Nif.Equals( nif ) ) == null ) {
                    Console.Write( "Ese nif no esta en la lista" );
                } else {
                    salida = true;
                }
            } while ( !salida );
            return nif;
        }

        private void MostrarUnEmpleado( List<Empleado> listaEmpleados, String nif ) {
            Empleado empleado = listaEmpleados.Find( ( Empleado arg ) => arg.Nif.Equals( nif ) );
            if ( empleado == null ) {
                Console.Write( "No esta el empleado con el nif " + nif );
            } else {
                Console.Write( empleado.MostrarEmpleado() );
            }
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
        private void CrearEmpleado( List<Empleado> listaEmpleados ) {
            bool salida = false;
            int edad;
            string nombre, apellido1, apellido2, nif;
            double salario;
            Console.Write( "Has entrado en el programa de creación de personal\n Introduce el nif del usuario" );
            do {
                nif = ped.PedirString();
                if ( listaEmpleados.Count == 0 || listaEmpleados.Find( ( Empleado obj ) => obj.Nif.Equals( nif ) ) == null ) {
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
            listaEmpleados.Add( new Empleado( nombre, apellido1, apellido2, edad, nif, salario ) );
            Console.Write( "El empleado ha sido creado" );
        }

        private bool ExisteNif( List<Empleado> listaEmpleados, string nombre ) {// true: si esta false: no esta
            bool resultadoExit = false;
            if ( listaEmpleados.Count > 0 ) {
                foreach ( Empleado persona in listaEmpleados ) {
                    if ( persona.Nombre.Equals( nombre ) ) {
                        resultadoExit = true;
                    }
                }
            }
            return resultadoExit;
        }

        public static void Main( string[] args ) {
            new MainClass().Tarea1();
        }
    }
}
