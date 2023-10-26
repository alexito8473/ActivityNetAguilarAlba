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
            Empresa empre = new Empresa();
            Menu( empre );
        }
        private void Menu(  Empresa empre ) {
            bool salida = false;
            do {
                Console.Write( "\n---Menu---\n0 -> Salir\n1 -> Crear empleado\n2 -> Actualizar salario\n3 -> Mostrar nombre\n4 -> Actualizar nombre\n5 -> Mostrar edad\n6 -> Modificar edad\n7 -> Eliminar empleado\n8 -> Mostrar empleado/Mostrar todos\n" );
                switch ( ped.PedirIntEnRango( 0, MAXIMO ) ) {
                    case SALIDA:
                        salida = true;
                        break;
                    case CREAR_EMPLEADO:
                        empre.CrearEmpleado();
                        break;
                    case ACTUALIZAR_SALARIO:
                        empre.ActualizarSalario( );
                        break;
                    case MOSTRAR_NOMBRE:
                        empre.AccederNombre(  true );
                        break;
                    case ACTUALIZAR_NOMBRE:
                        empre.AccederNombre(  false );
                        break;
                    case MOSTRAR_EDAD:
                        empre.AccederEdad(  true );
                        break;
                    case MODIFCAR_EDAD:
                        empre.AccederEdad(  false );
                        break;
                    case ELIMINAR_EMPLEADO:
                        empre.EliminarEmpleado(  );
                        break;
                    case MOSTRAR_EMPLEADO:
                        empre.ComoMostrarEmpleado(  );
                        break;
                }
            } while ( !salida );
            Console.Write( "Se cerro el programa" );
        }

        public static void Main( string[] args ) {
            new MainClass().Tarea1();
        }
    }
}
