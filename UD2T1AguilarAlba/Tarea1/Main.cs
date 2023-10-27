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
        private const int MODIFICAR_EDAD = 6;
        private const int ELIMINAR_EMPLEADO = 7;
        private const int MOSTRAR_EMPLEADO = 8;

        private string TEXTO_SALIDA = string.Format($"\t\t{SALIDA} -> Salir\n" );
        private string TEXTO_CREAR_EMPLEADO = string.Format( $"\t\t{CREAR_EMPLEADO} -> Crear empleado\n" );
        private string TEXTO_ACTUALIZAR_SALARIO = string.Format( $"\t\t{ACTUALIZAR_SALARIO} -> Actualizar salario\n" );
        private string TEXTO_MOSTRAR_NOMBRE = string.Format( $"\t\t{MOSTRAR_NOMBRE} -> Mostrar nombre\n" );
        private string TEXTO_ACTUALIZAR_NOMBRE = string.Format( $"\t\t{ACTUALIZAR_NOMBRE} -> Actualizar nombre\n" );
        private string TEXTO_MOSTRAR_EDAD = string.Format( $"\t\t{MOSTRAR_EDAD} -> Mostrar edad\n" );
        private string TEXTO_MODIFCAR_EDAD = string.Format( $"\t\t{MODIFICAR_EDAD} -> Modificar edad\n" );
        private string TEXTO_ELIMINAR_EMPLEADO = string.Format( $"\t\t{ELIMINAR_EMPLEADO} -> Eliminar empleado\n" );
        private string TEXTO_MOSTRAR_EMPLEADO = string.Format( $"\t\t{MOSTRAR_EMPLEADO} -> Mostrar empleado [NIF] / todos los empleados\n" );
        private Pedirdatos ped = new Pedirdatos();

        private void Tarea1() {
            Empresa empre = new Empresa();
            Menu( empre );
        }

        private void MostrarMenu() {
            Console.ResetColor();
            Console.Write( "\n\t\t  ---Super Menu---\n" );
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write( "{0}{1}{2}", TEXTO_SALIDA, TEXTO_CREAR_EMPLEADO, TEXTO_ACTUALIZAR_SALARIO );
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write( "{0}{1}{2}", TEXTO_MOSTRAR_NOMBRE, TEXTO_ACTUALIZAR_NOMBRE, TEXTO_MOSTRAR_EDAD );
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write( "{0}{1}{2}\n", TEXTO_MODIFCAR_EDAD, TEXTO_ELIMINAR_EMPLEADO , TEXTO_MOSTRAR_EMPLEADO );
            Console.ResetColor();
        }

        private void Menu(  Empresa empre ) {
            bool salida = false;
            bool salidaMini = false;
            string nif;
            do {
                MostrarMenu();
                switch ( ped.PedirIntEnRango( 0, MAXIMO ) ) {
                    case SALIDA:
                        salida = true;
                        break;
                    case CREAR_EMPLEADO:
                        empre.CrearEmpleado();
                        break;
                    case ACTUALIZAR_SALARIO:
                        if ( empre.GetListEmpleados().Count>0 ) {
                            empre.ActualizarSalario( ped.PedirDoublePositivo( "Que salario quieres introducir al nuevo usuario" ) );
                        } else {
                            Console.Write( "No hay empleados" );
                        }
                        break;
                    case MOSTRAR_NOMBRE:
                        empre.MostrarNombre(  );
                        break;
                    case ACTUALIZAR_NOMBRE:
                        empre.ActualizarNombre( );
                        break;
                    case MOSTRAR_EDAD:
                        if ( empre.GetListEmpleados().Count == 0 ) {
                            Console.Write( "No hay empleados" );
                        } else {
                            salidaMini = false;
                            empre.MostrarListaNif();
                            do {
                                if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                                    if ( empre.ExisteNif( nif ) ) {
                                        Console.Write( "Su edad es -> " );
                                        empre.MostrarEdad(empre.DevolverEmpleado(nif));
                                        salidaMini = true;
                                    } else {
                                        Console.Write( "No existe el usuario" );
                                    }
                                } else {
                                    salidaMini = true;
                                    Console.Write( "Saliendo...\n" );
                                }
                            } while ( !salidaMini );
                        }    
                        break;
                    case MODIFICAR_EDAD:
                        if ( empre.GetListEmpleados().Count == 0 ) {
                            Console.Write( "No hay empleados" );
                        } else {
                            salidaMini = false;
                            empre.MostrarListaNif();
                            do {
                                if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                                    if ( empre.ExisteNif( nif ) ) {
                                        empre.ModificarEdad( empre.DevolverEmpleado( nif ) );
                                        Console.Write( "Edad modificada" );
                                        salidaMini = true;
                                    } else {
                                        Console.Write( "No existe el usuario" );
                                    }
                                } else {
                                    salidaMini = true;
                                    Console.Write( "Saliendo...\n" );
                                }
                            } while ( !salidaMini );
                        }
                        break;
                    case ELIMINAR_EMPLEADO:
                        if ( empre.GetListEmpleados().Count == 0 ) {
                            Console.Write( "No hay empleados" );
                        } else {
                            salidaMini = false;
                            empre.MostrarListaNif();
                            do {
                                if ( ( nif = ped.PedirStringSinControl( "\nPasame el nif que quieras buscar" ) ).Length > 0 ) {
                                    if ( empre.ExisteNif( nif ) ) {
                                        empre.EliminarEmpleado( empre.DevolverEmpleado( nif ) );
                                        Console.Write( "Usuario eliminado" );
                                        salidaMini = true;
                                    } else {
                                        Console.Write( "No existe el usuario" );
                                    }
                                } else {
                                    salidaMini = true;
                                    Console.Write( "Saliendo...\n" );
                                }
                            } while ( !salidaMini );
                        }
                        break;
                    case MOSTRAR_EMPLEADO:
                        empre.ComoMostrarEmpleado(  );
                        break;
                }
            } while ( !salida );
            empre.EscribrirEmpleadosFichero();
            Console.Write( "Se cerro el programa" );
        }

        public static void Main( string[] args ) {
            new MainClass().Tarea1();
        }
    }
}
