using System;
using System.Collections.Generic;

namespace To_Do_List
{
    // clase encapsulada que representa una tarea
    class Tarea
    {
        public string Nombre { get; set; } // nombre de la tarea
        public bool Completada { get; set; } // estado de la tarea
        public DateTime? FechaLimite { get; set; } // fecha limite de lat area

        public Tarea(string nombre, DateTime? fechaLimite = null)
        {
            Nombre = nombre;
            Completada = false;
            FechaLimite = fechaLimite;
        }

        public override string ToString()
        {
            string estado = Completada ? "[X]" : "[ ]"; // como se representa el estado en la tarea
            string fecha = FechaLimite.HasValue ? FechaLimite.Value.ToString("yyyy-MM-dd") : "sin fecha";
            return $"{estado} {Nombre} (fecha limite: {fecha})";
        }
    }

    // clase que representa la aplicacion de to-do
    class TodoApp
    {
        private List<Tarea> tareas; // lista de tareas como objeto

        public TodoApp()
        {
            tareas = new List<Tarea>(); // se crea una nueva lista
        }

        public void Mostrar() // metodo para mostrar tareas
        {
            Console.Clear();
            Console.WriteLine("== lista de tareas ==");

            if (tareas.Count == 0) // comprobacion para ver si no hay tareas
            {
                Console.WriteLine("no hay tareas ingresadas aun.");
            }
            else
            {
                for (int i = 0; i < tareas.Count; i++) // si hay tareas inicia el for para mostrarlas
                {
                    Console.WriteLine($"{i + 1}. {tareas[i]}"); // linea que ingresa las tareas
                }
            }
            Console.WriteLine("presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void Agregar() // metodo para ingresar tareas
        {
            Console.Clear();
            Console.WriteLine("ingrese la descripcion de la tarea:");
            string nueva = Console.ReadLine(); // pasamos lo ingresado a la variable string nueva

            if (string.IsNullOrWhiteSpace(nueva))
            {
                Console.WriteLine("no se ha ingresado una tarea valida."); // si esta vacia o nula no ingresa nada
            }
            else
            {
                DateTime? fechaLimite = null;
                bool fechaValida = false;

                // Bucle para pedir una fecha válida
                while (!fechaValida)
                {
                    Console.WriteLine("ingrese la fecha limite de la tarea (opcional, formato yyyy-MM-dd):");
                    string fechaEntrada = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(fechaEntrada)) // si no se ingresa una fecha, se sale del bucle
                    {
                        fechaValida = true;
                    }
                    else if (DateTime.TryParse(fechaEntrada, out DateTime fecha)) // verifica si la fecha es válida
                    {
                        fechaLimite = fecha;
                        fechaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Fecha no válida. Por favor, ingrese una fecha en el formato yyyy-MM-dd.");
                    }
                }

                tareas.Add(new Tarea(nueva, fechaLimite)); // añade la nueva tarea a la lista si tiene datos la variable nueva
                Console.WriteLine("se ha ingresado la tarea correctamente.");
            }
            Console.WriteLine("presiona una tecla para continuar...");
            Console.ReadKey();
        }


        public void Marcar() // metodo para marcar las tareas como completadas o incompletas
        {
            Console.Clear();
            Mostrar(); // muestra las tareas antes de solicitar el numero de tarea
            if (tareas.Count == 0) return; // si esta vacia hacemos return

            Console.Write("ingresa el numero de la tarea para marcarla como completada/no completada: ");
            if (int.TryParse(Console.ReadLine(), out int numeroTarea) && numeroTarea > 0 && numeroTarea <= tareas.Count)
            //convertimos a int y si sale bien lo almacenamos en nuevatarea para ya ahcer validaciones
            {
                tareas[numeroTarea - 1].Completada = !tareas[numeroTarea - 1].Completada; // cambia el estado de la tarea
                Console.WriteLine("estado de la tarea actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine("numero de tarea invalido."); // si no se almacena un numero en la variable no marcamos nada
            }
            Console.WriteLine("presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void Eliminar() // metodo para eliminar tareas
        {
            Console.Clear();
            Mostrar(); // muestra las tareas antes de solicitar el numero de tarea
            if (tareas.Count == 0) return; // si esta vacia hacemos return

            Console.Write("ingresa el numero de la tarea para eliminarla: ");
            if (int.TryParse(Console.ReadLine(), out int numeroTarea) && numeroTarea > 0 && numeroTarea <= tareas.Count)
            {
                tareas.RemoveAt(numeroTarea - 1); // elimina la tarea de la lista
                Console.WriteLine("tarea eliminada exitosamente.");
            }
            else
            {
                Console.WriteLine("numero de tarea invalido."); // si no se almacena un numero en la variable no eliminamos nada y saca invalido
            }
            Console.WriteLine("presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    // clase principal
    class Program
    {
        static void Main()
        {
            TodoApp app = new TodoApp(); // creamos un nuevo objeto
            bool continuar = true; // variable para mantener el while activo

            while (continuar) // ciclo while para switch case como un menu interactivo
            {
                Console.Clear(); // limpiamos la consola
                Console.WriteLine("== to-do list =="); // menu de opciones
                Console.WriteLine("1. ver tareas ingresadas");
                Console.WriteLine("2. agregar una tarea");
                Console.WriteLine("3. marcar una tarea");
                Console.WriteLine("4. eliminar una tarea");
                Console.WriteLine("5. salir del to-do list");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        app.Mostrar(); // llamamos al metodo mostrar
                        break;
                    case "2":
                        app.Agregar(); // llamamos al metodo agregar
                        break;
                    case "3":
                        app.Marcar(); // llamamos al metodo marcar
                        break;
                    case "4":
                        app.Eliminar(); // llamamos al metodo eliminar
                        break;
                    case "5":
                        continuar = false; // devolvemos false para terminar el while y salir del programa
                        break;
                    default:
                        Console.WriteLine("opcion no valida, ingrese nuevamente."); //si ingresa uno invalido volvemos al menu
                        break;
                }
            }
        }
    }
}
