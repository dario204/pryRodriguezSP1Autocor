using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pryRodriguezSP1Autocor
{
    internal class clsArchivo
    {
        public String NombreArchivo { get; set; }
        public bool GrabarRepuesto(clsRepuesto repuesto)
        {
            bool resultado = false;
            if (NombreArchivo != "")
            {
                StreamWriter sw = new StreamWriter(NombreArchivo, true);
                sw.WriteLine(repuesto.Codigo + "," + repuesto.Nombre + "," + repuesto.Marca + "," +
                    repuesto.Precio.ToString("#.00", CultureInfo.InvariantCulture) + "," + repuesto.Origen);
                sw.Close();
                sw.Dispose();
                resultado = true;

            }
            return resultado;
        }
        public bool BuscarCodigoRepuesto(string codigo)
        {
            // recibe el código del repuesto a buscar
            // devuelve falso si el código no existe en el archivo
            // devuelve verdadero si el código ya está grabado
            bool resultado = false;
            string Linea;
            string CodigoRepuesto;
            // verificar que el archivo existe
            if (NombreArchivo != "" && File.Exists(NombreArchivo))
            {
                // crear el stream en modo lectura
                StreamReader sr = new StreamReader(NombreArchivo);
                // leer hasta el final
                while (sr.EndOfStream == false)
                {
                    Linea = sr.ReadLine(); // lee una linea completa
                                           // el código está en el primer valor de cada línea
                    CodigoRepuesto = Linea.Split(',')[0];
                    // comparar el código buscado con el del archivo
                    if (codigo == CodigoRepuesto)
                    {
                        // si son iguales devuelve verdadero
                        resultado = true;
                        break; // sale del ciclo de lectura
                    }
                }
                sr.Close(); // cerrar el stream
                sr.Dispose(); // liberar los recursos
            }
            return resultado;
        }


        public List<clsRepuesto> ObtenerRepuestos()
        {
            // lee el contenido completo del archivo y lo
            // almacena en una lista de objetos 'Repuesto'
            List<clsRepuesto> Lista = new List<clsRepuesto>();
            string Linea;
            if (NombreArchivo != "" && File.Exists(NombreArchivo))
            {
                StreamReader sr = new StreamReader(NombreArchivo);
                while (sr.EndOfStream == false)
                {
                    Linea = sr.ReadLine(); // lee una linea del archivo
                    clsRepuesto repuesto = new clsRepuesto();
                    repuesto.Codigo = Linea.Split(',')[0];
                    repuesto.Nombre = Linea.Split(',')[1];
                    repuesto.Marca = Linea.Split(',')[2];
                    // el valor decimal se formatea sin formatos regionales
                    // para mantener el punto como separador decimal
                    repuesto.Precio = decimal.Parse(Linea.Split(',')[3],
                    CultureInfo.InvariantCulture);
                    repuesto.Origen = Linea.Split(',')[4];
                    Lista.Add(repuesto); // se agrega el repuesto a la lista
                }
                sr.Close(); // cerrar
                sr.Dispose(); // liberar recursos
            }
            // devuelve la lista de repuestos completa
            return Lista;
        }

        public List<clsRepuesto> ObtenerRepuestosOrdenados()
        {
            // lee el contenido completo del archivo y lo
            // almacena en una lista de objetos 'Repuesto'
            List<clsRepuesto> Lista = ObtenerRepuestos();
            // convertir la lista en un arreglo con el método "ToArray()"
            clsRepuesto[] repuestosArray = Lista.ToArray();
            // ordenar el arreglo con el método de Burbuja
            // por el campo Nombre en forma ascendente (de menor a mayor)
            for (int i = 0; i < repuestosArray.Length - 1; i++)
            {
                for (int j = 0; j < repuestosArray.Length - 1; j++)
                {
                    // se comparan los nombres de los repuestos
                    // usando el método 'Compare' de la clase string
                    if (string.Compare(repuestosArray[j].Nombre,
                    repuestosArray[j + 1].Nombre) > 0)
                    {
                        // se intercambian si el nombre en j es mayor al nombre en j+1
                        clsRepuesto aux = repuestosArray[j];
                        repuestosArray[j] = repuestosArray[j + 1];
                        repuestosArray[j + 1] = aux;
                    }
                }
            }
            // convertir el arreglo ya ordenado en una lista
            List<clsRepuesto> Ordenados = repuestosArray.ToList<clsRepuesto>();
            // devuelve la lista de respuestos ordenada por nombre en forma ascendente
            return Ordenados;
        }
        // definimos en nombre del archivo en una constante
        
    } }
