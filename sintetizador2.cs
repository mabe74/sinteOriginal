using System.Text;
using System.Collections.Generic;
using NAudio.Wave;


namespace sintetizador2
{
    public enum ModelosSintes
    {
        MonoPoly = 1,
        ARP2600 = 2,
        Model_D = 3,
        Solina = 4,
        Oddissey = 5,
        MS_5 = 6,
        MS_101 = 7,
        Generico
    }




    internal class sintetizador2
    {
 
        //Atributos de instancia (cada sinte tienelos suyos)
        private bool _encendido;

        private ModelosSintes _modelo;
        private bool _tieneTeclas;
        private int _numeroDeTeclas;
        private bool _tienePantalla;
        private int _tensionDeTrabajo;
        private string _tipoDeTensionDeTrabajo;
        private int _nivelBateria; //Nivel de bateria del sintetizador (0-100)
        private string _estadoBateriaMensaje;
        private string _mensajeBateria;
        private int _osciladores; //Numero de osciladores
        private int _polifonia; //Numero de notas que puede tocar al mismo tiempo
        private int _opcion; //Elije el sinte

        //================================================================================================================

        //Atributos estáticos (comunes a todos los sintes)
        private static int _cantidadDeSintes = 0;
        private static string _fabricante = "Behringer";
        private static string versionFirmeware = "version 1.0.0";



        //Crea la lista de sintetizadores
        private static List<sintetizador2> listaDeSintes = new List<sintetizador2>();




        //=================================================================================================================

        //Constructor
        public sintetizador2(ModelosSintes modelo, bool tieneTeclas, int numeroDeTeclas, bool tienePantalla, int tensionDeTrabajo,
                             string tipoDeTensionDeTrabajo, int osciladores, int polifonia)

        {
            this._encendido = false; //Valor por defecto
             
            this._modelo = modelo;
            this._tieneTeclas = tieneTeclas;
            this._numeroDeTeclas = numeroDeTeclas;
            this._tienePantalla = tienePantalla;
            this._tensionDeTrabajo = tensionDeTrabajo;
            this._tipoDeTensionDeTrabajo = tipoDeTensionDeTrabajo;
            this._nivelBateria = 100; //Valor por defecto
            this._estadoBateriaMensaje = "";
            this._mensajeBateria = "";
            this._osciladores = osciladores;
            this._polifonia = polifonia;
            _cantidadDeSintes++;// ver para constructor estatico
        }

        //===================================================================================================================

        //Sobrecarga de constructores

        

         //Sobrecarga 1: modelo y osciladores
         public sintetizador2(ModelosSintes modelo, int osciladores)

                : this(modelo, false, 0, false, 12, "DC", osciladores, 4)
         {
                // Valores por defecto: sin teclas, sin pantalla, 12V DC, 4 notas de polifonía
         }

         //Sobrecarga 2: modelo solamente
         public sintetizador2(ModelosSintes modelo)
                : this(modelo, false, 0, false, 12, "DC", 2, 4)
         {
                // Modelo básico, sin teclas ni pantalla
         }

         //Sobrecarga 3: sin parámetros (usa valores por defecto)
         public sintetizador2()
                : this(ModelosSintes.Generico, false, 0, false, 12, "DC", 2, 4)
         {
                // Si no se especifica, se crea un MonoPoly básico
         }

        //Sobrecarga 4: modelos y teclas: si / no
        public sintetizador2(ModelosSintes modelo, bool tieneTeclas)

            : this(ModelosSintes.Solina, false, 0, false, 12, "DC", 2, 4)
        {
        }

       //===========================================================================================================================

        // Propiedades estáticas
        public static int CantidadDeSintes => _cantidadDeSintes;
        public static string Fabricante => _fabricante;

        //===========================================================================================================================

        public static string Saludar()
        {
            StringBuilder saludar = new StringBuilder();

            saludar.AppendLine("\nBienvenido!\n");


            return saludar.ToString();
        }

        //===========================================================================================================================
        
        
        
        //Carga las máquinas en la lista
        public static void CargarMaquinas()
        {
            listaDeSintes.Clear();

            listaDeSintes.Add(new sintetizador2(ModelosSintes.MonoPoly, true, 37, true, 12, "DC", 2, 4));
            listaDeSintes.Add(new sintetizador2(ModelosSintes.ARP2600, false, 0, false, 15, "AC", 3, 6));
            listaDeSintes.Add(new sintetizador2(ModelosSintes.Model_D, true, 32, true, 9, "DC", 3, 4));
            listaDeSintes.Add(new sintetizador2(ModelosSintes.Solina, true, 49, true, 12, "DC", 2, 8));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Máquinas cargadas correctamente");
        }


        public static List<sintetizador2> ObtenerLista()
        {

            return listaDeSintes;

        }





        //===========================================================================================================================

        
        
        
        
       

        //Sintetizadores
        public string EncenderSinte(bool encendido)
        {
            this._encendido = encendido;

            if (_encendido)
            {

                StringBuilder info1 = new StringBuilder();

                info1.AppendLine($"{_modelo}: ON");


                //info1.AppendLine($"Modelo: {_modelo}");
                info1.AppendLine($"Pantalla: {_tienePantalla}");
                info1.AppendLine($"Tensión: {_tensionDeTrabajo} volts");
                info1.AppendLine($"Tipo de tensión: {_tipoDeTensionDeTrabajo}");
                info1.AppendLine($"Cantidad de osciladores: {_osciladores}");
                info1.AppendLine($"Polifonia: {_polifonia}");

                if (_tieneTeclas)
                {

                    info1.AppendLine($"Número de teclas: {_numeroDeTeclas}");

                }
                else
                {

                    info1.AppendLine($"No posee teclas");

                }


                return info1.ToString();


            }
            else
            {
                StringBuilder info1 = new StringBuilder();
                info1.AppendLine("Sintetizador 1: OFF");
                return info1.ToString();
            }

        }

        //=========================================================================================================================

        public string ChequearBateriaMensaje()
        {
            StringBuilder chequearBateria = new StringBuilder();
            chequearBateria.Append("\nChequeando batería");
            return chequearBateria.ToString();

        }

        //=========================================================================================================================

        public void MoverPuntos()
        {


            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500); // medio segundo de espera (delay en milisegundos)
                Console.Write("."); // imprime un punto sin salto de línea
            }


            //Console.WriteLine(); // para terminar la línea
        }

        //=========================================================================================================================
        //A partir de esta línea
        public class SineWaveProvider : WaveProvider32
        {
            private float frequency;
            private float amplitude = 0.25f;
            private float phase;

            public SineWaveProvider(float frequency = 440f)
            {
                this.frequency = frequency;
            }

            public override int Read(float[] buffer, int offset, int count)
            {
                for (int n = 0; n < count; n++)
                {
                    buffer[n + offset] = amplitude * (float)Math.Sin(phase);
                    phase += (float)(2 * Math.PI * frequency / WaveFormat.SampleRate);
                    if (phase > 2 * Math.PI);
                        //phase -= 2 * Math.PI;
                }
                return count;
            }

            public void SetFrequency(float freq)
            {
                frequency = freq;
            }
        }

        public void GenerarOndaSenoAnimada()
        {
            if (_encendido)
            {
                int ancho = 80;
                int alto = 20;
                double frecuencia = 2 * Math.PI / 40;
                int desplazamiento = 0;

                // AUDIO SENO REAL
                var seno = new SineWaveProvider(440f);
                WaveOutEvent waveOut = new WaveOutEvent();
                waveOut.Init(seno);
                waveOut.Play();

                while (true)
                {
                    Thread.Sleep(200);
                    Console.Clear();

                    for (int y = 0; y < alto; y++)
                    {
                        for (int x = 0; x < ancho; x++)
                        {
                            double valor = Math.Sin((x + desplazamiento) * frecuencia);
                            int posY = (int)((valor + 1) * (alto - 1) / 2);

                            if (alto - y - 1 == posY)
                                Console.Write("*");
                            else
                                Console.Write(" ");
                        }
                        Console.WriteLine();
                    }

                    desplazamiento++;

                    // si querés modificar frecuencia según la animación:
                    // seno.SetFrequency(440f + desplazamiento);

                }
            }
        }








        //Hasta esta línea
        //=============================================================================================================================
        public void GenerarOndaTriangular()
        {






        }

        //=============================================================================================================================

        //Autoreferenciada
        public int NivelBateria
        {
            get
            {
                // Si está fuera de rango, autoregula el valor
                if (_nivelBateria < 0) _nivelBateria = 0;
                else if (_nivelBateria > 100) _nivelBateria = 100;
                return _nivelBateria;
            }
            set
            {
                _nivelBateria = value;
            }
        }



        //Propiedad autocalculada
        public string MostrarEstadoBateriaPorcentaje
            {
                get
                {
                    if (_nivelBateria == 100)
                        return "Estado de batería completo";
                    else if (_nivelBateria >= 66)
                        return "Estado batería: OK +";
                    else if (_nivelBateria >= 33)
                        return "Estado batería: OK -";
                    else
                        return "Recargar batería";
                }
            }






        
        //=============================================================================================================================
        //=============================================================================================================================
            /*
            //Metodos Get y Set (propiedades)
            //Comprueba el estado de la batería

            public int EstadoBateria

        {
            get { return this._nivelBateria; }

            set
            {
                StringBuilder sb = new StringBuilder();

                //Normaliza el nivel de bateria
                if (value < 0)
                {
                    value = 0;
                }

                if (value > 100)
                {
                    value = 100;
                }

                //Asigna el valor al atributo
                this._nivelBateria = value;

                //Comprueba el estado de la bateria
                if (value == 100)
                {

                    this._nivelBateria = value;
                   

                }
                else if (value >= 66)
                {

                    this._nivelBateria = value;
                   

                }
                else if (value >= 33)
                {

                    this._nivelBateria = value;
                    

                }

                else
                {
                    this._nivelBateria = value;
                }

            }
        }
        */
        
        //=========================================================================================================================

        //Propiedad autoreferenciada

        /*
        public string MostrarEstadoBateria
        {

            get { return this._mensajeBateria; }



            set
            {
                StringBuilder sb = new StringBuilder();

                if (this._nivelBateria == 100)
                {
                    sb.AppendLine("Estado de batería completo");

                }
                else if (this._nivelBateria >= 66)
                {

                    sb.AppendLine("Estado batería: OK + ");

                }
                else if (this._nivelBateria >=33)
                {

                    sb.AppendLine("Estado batería: OK - ");
                
                }
                else
                {

                    sb.AppendLine("Recargar batería");

                }

                this._mensajeBateria = sb.ToString();
            }

        

            }
       */ }
    }


/*
 
Autoreferenciada:


¿Qué es una propiedad autoreferenciada?

Autoreferenciada suele referirse a que la propiedad se refiere a sí misma dentro de su definición, 
lo que es raro o incluso problemático si se hace incorrectamente (por ejemplo, un get que se llama a sí 
mismo causaría recursión infinita).


 public int NivelBateria
{
    get
    {
        // Si está fuera de rango, autoregula el valor
        if (NivelBateria < 0) NivelBateria = 0;
        else if (NivelBateria > 100) NivelBateria = 100;
        return _nivelBateria;
    }
    set
    {
        _nivelBateria = value;
    }
}

 Explicación:

Aquí, en el get de la propiedad Valor, llamamos al set de la misma propiedad Valor (autoreferencia).

La llamada se controla para evitar recursión infinita porque el set cambia el campo privado _valor.

Esto hace que la propiedad "se refiera a sí misma" para ajustar o validar el valor.


================================================================================================================

¿Qué es una propiedad autocalculada?

Autocalculada: es una propiedad cuyo valor no se guarda explícitamente, sino que se calcula en tiempo real en
base a otras variables o estados internos.

Ejemplo típico: una propiedad que devuelve el resultado de una operación entre campos o propiedades internas, 
sin necesidad de almacenar ese resultado en una variable de respaldo.

=======================================================================================================================
=======================================================================================================================
Listas


Para implementar listas en el método CargarMaquinas() de tu código, puedes usar una lista (List<T>) en C# para 
almacenar y gestionar objetos de la clase sintetizador2. Esto te permitirá crear y manejar una colección de 
sintetizadores con diferentes configuraciones. A continuación, te muestro cómo implementar el método CargarMaquinas() 
usando una lista, junto con una explicación detallada.

Implementación del método CargarMaquinas() con listas:

using System.Collections.Generic; // Necesario para usar List<T>

public class sintetizador2
{
    // ... (resto del código existente)

    // Lista para almacenar los sintetizadores
    private List<sintetizador2> _sintetizadores;

    // Constructor modificado para inicializar la lista
    public sintetizador2(ModelosSintes modelo, bool tieneTeclas, int numeroDeTeclas, bool tienePantalla, int tensionDeTrabajo,
                         string tipoDeTensionDeTrabajo, int osciladores, int polifonia)
    {
        this._encendido = false;
        this._modelo = modelo;
        this._tieneTeclas = tieneTeclas;
        this._numeroDeTeclas = numeroDeTeclas;
        this._tienePantalla = tienePantalla;
        this._tensionDeTrabajo = tensionDeTrabajo;
        this._tipoDeTensionDeTrabajo = tipoDeTensionDeTrabajo;
        this._nivelBateria = 100;
        this._estadoBateriaMensaje = "";
        this._mensajeBateria = "";
        this._osciladores = osciladores;
        this._polifonia = polifonia;
        _cantidadDeSintes++;
        
        // Inicializar la lista
        _sintetizadores = new List<sintetizador2>();
    }

    // Sobrecarga de constructores (deben inicializar la lista también)
    public sintetizador2(ModelosSintes modelo, int osciladores)
        : this(modelo, false, 0, false, 12, "DC", osciladores, 4)
    {
        _sintetizadores = new List<sintetizador2>();
    }

    public sintetizador2(ModelosSintes modelo)
        : this(modelo, false, 0, false, 12, "DC", 2, 4)
    {
        _sintetizadores = new List<sintetizador2>();
    }

    public sintetizador2()
        : this(ModelosSintes.MonoPoly, false, 0, false, 12, "DC", 2, 4)
    {
        _sintetizadores = new List<sintetizador2>();
    }

    // Método para cargar sintetizadores en la lista
    public void CargarMaquinas()
    {
        // Limpiar la lista antes de cargar nuevas máquinas (opcional)
        _sintetizadores.Clear();

        // Crear instancias de sintetizadores con diferentes configuraciones
        _sintetizadores.Add(new sintetizador2(ModelosSintes.MonoPoly, true, 61, true, 12, "DC", 2, 4));
        _sintetizadores.Add(new sintetizador2(ModelosSintes.ARP2600, false, 0, false, 12, "DC", 3, 2));
        _sintetizadores.Add(new sintetizador2(ModelosSintes.Model_D, true, 37, false, 9, "DC", 3, 1));
        _sintetizadores.Add(new sintetizador2(ModelosSintes.Solina, true, 49, true, 12, "DC", 2, 8));
        _sintetizadores.Add(new sintetizador2(ModelosSintes.Oddissey, true, 37, false, 12, "DC", 2, 2));
        _sintetizadores.Add(new sintetizador2(ModelosSintes.MS_5, true, 61, true, 12, "DC", 2, 6));
        _sintetizadores.Add(new sintetizador2(ModelosSintes.MS_101, true, 32, false, 9, "DC", 1, 1));

        // Mostrar información de los sintetizadores cargados
        Console.WriteLine("\nSintetizadores cargados:");
        for (int i = 0; i < _sintetizadores.Count; i++)
        {
            Console.WriteLine($"\nSintetizador {i + 1}:");
            Console.WriteLine(_sintetizadores[i].EncenderSinte(true));
        }
    }
}


Explicación del código

Importar el namespace para listas:

Se agrega using System.Collections.Generic; para usar la clase List<T>.


Declarar la lista:

Se añade un atributo privado _sintetizadores de tipo List<sintetizador2> para almacenar los sintetizadores.


Inicializar la lista en los constructores:

En cada constructor, se inicializa _sintetizadores = new List<sintetizador2>(); para asegurar que la lista esté lista para 
usar.


Método CargarMaquinas():

Limpieza (opcional): _sintetizadores.Clear() asegura que la lista esté vacía antes de cargar nuevas máquinas.
Agregar sintetizadores: Se crean instancias de sintetizador2 con diferentes configuraciones usando el constructor 
completo y se añaden a la lista con _sintetizadores.Add().
Mostrar información: Se recorre la lista con un bucle for y se llama al método EncenderSinte(true) para mostrar 
los detalles de cada sintetizador.


Configuraciones de ejemplo:

Cada sintetizador se configura con valores realistas para los parámetros como número de teclas, osciladores, polifonía, etc., según el modelo.



Uso de la lista en otros métodos:

Si deseas trabajar con la lista en otros métodos (por ejemplo, para seleccionar un sintetizador), 
puedes acceder a _sintetizadores y usar métodos de List<T> como:

_sintetizadores.Count: Obtener el número de sintetizadores.
_sintetizadores[i]: Acceder a un sintetizador por índice.
_sintetizadores.RemoveAt(i): Eliminar un sintetizador.
_sintetizadores.Find(s => s._modelo == ModelosSintes.MonoPoly): Buscar un sintetizador por modelo.

 Por ejemplo, para reimplementar el método comentado ElegirSinte:

public void ElegirSinte()
{
    Console.WriteLine("\nSeleccione un sintetizador:");
    for (int i = 0; i < _sintetizadores.Count; i++)
    {
        Console.WriteLine($"{i + 1}.- {_sintetizadores[i]._modelo}");
    }

    Console.Write("\nIngrese el número del sintetizador: ");
    if (int.TryParse(Console.ReadLine(), out int opcion) && opcion > 0 && opcion <= _sintetizadores.Count)
    {
        Console.WriteLine("\nSintetizador seleccionado:");
        Console.WriteLine(_sintetizadores[opcion - 1].EncenderSinte(true));
    }
    else
    {
        Console.WriteLine("Opción inválida.");
    }
}

=======================================================================================================================
=======================================================================================================================
Herencia

¡Muy bien! Tu código está bastante completo y bien estructurado, pero quieres aplicar herencia para aprovechar mejor la 
programación orientada a objetos y evitar repetir código entre modelos de sintetizadores.
Actualmente tienes una clase sintetizador2 que representa todos los modelos mediante un enum ModelosSintes. Esto funciona, 
pero no aprovecha la herencia. Vamos a refactorizarlo usando herencia para que:

Cada modelo de sintetizador sea una clase derivada.
Los atributos comunes estén en una clase base.
Los comportamientos específicos (como osciladores, polifonía, teclas, etc.) se definan por modelo.

PASO 1: Clase base abstracta Sintetizador

using System.Text;

namespace sintetizador2
{
    public abstract class Sintetizador
    {
        // Atributos comunes a todos los sintetizadores
        protected bool _encendido;
        protected int _nivelBateria;
        protected string _estadoBateriaMensaje;
        protected string _mensajeBateria;

        // Atributos estáticos comunes
        private static int _cantidadDeSintes = 0;
        private static readonly string _fabricante = "Behringer";
        private static readonly string _versionFirmware = "version 1.0.0";

        // Constructor base
        protected Sintetizador()
        {
            _encendido = false;
            _nivelBateria = 100;
            _cantidadDeSintes++;
        }

        // Propiedades estáticas
        public static int CantidadDeSintes => _cantidadDeSintes;
        public static string Fabricante => _fabricante;
        public static string VersionFirmware => _versionFirmware;

        // Propiedades de instancia
        public bool Encendido => _encendido;
        public int NivelBateria
        {
            get => _nivelBateria < 0 ? 0 : _nivelBateria > 100 ? 100 : _nivelBateria;
            set => _nivelBateria = value;
        }

        public string EstadoBateriaPorcentaje
        {
            get
            {
                if (_nivelBateria == 100) return "Batería completa";
                if (_nivelBateria >= 66) return "Batería: OK +";
                if (_nivelBateria >= 33) return "Batería: OK -";
                return "Recargar batería";
            }
        }

        // Métodos comunes
        public virtual string Encender()
        {
            _encendido = true;
            return $"{GetType().Name}: ON";
        }

        public virtual string Apagar()
        {
            _encendido = false;
            return $"{GetType().Name}: OFF";
        }

        public virtual string MostrarInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name}: {(Encendido ? "ON" : "OFF")}");
            sb.AppendLine($"Batería: {NivelBateria}% - {EstadoBateriaPorcentaje}");
            return sb.ToString();
        }

        public abstract int Osciladores { get; }
        public abstract int Polifonia { get; }
        public abstract bool TieneTeclas { get; }
        public abstract int NumeroDeTeclas { get; }
        public abstract bool TienePantalla { get; }
        public abstract int TensionDeTrabajo { get; }
        public abstract string TipoDeTension { get; }

        // Métodos de animación (pueden ser sobrescritos si un modelo tiene onda especial)
        public virtual void GenerarOndaSenoAnimada()
        {
            if (!_encendido) return;

            Console.Clear();
            Console.WriteLine("Generando onda senoidal genérica...");
            // Aquí iría tu animación (puedes moverla a una clase de ayuda si quieres)
        }

        public virtual void GenerarOndaTriangular()
        {
            if (!_encendido) return;
            Console.WriteLine("Onda triangular no implementada en este modelo.");
        }
    }
}

PASO 2: Clases derivadas por modelo

// MonoPoly
public class MonoPoly : Sintetizador
{
    public override int Osciladores => 1;
    public override int Polifonia => 1;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 37;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: MonoPoly (Monofónico)");
        sb.AppendLine($"Osciladores: {Osciladores}, Polifonía: {Polifonia}");
        sb.AppendLine($"Teclas: {NumeroDeTeclas}, Tensión: {TensionDeTrabajo}V {TipoDeTension}");
        return sb.ToString();
    }
}

// ARP2600
public class ARP2600 : Sintetizador
{
    public override int Osciladores => 2;
    public override int Polifonia => 1;
    public override bool TieneTeclas => false;
    public override int NumeroDeTeclas => 0;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 15;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: ARP2600 (Semi-modular)");
        sb.AppendLine($"Osciladores: {Osciladores}, Duofónico limitado");
        return sb.ToString();
    }
}

// Model D
public class ModelD : Sintetizador
{
    public override int Osciladores => 3;
    public override int Polifonia => 1;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 0; // Mini teclas o externo
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: Model D (Minimoog clone)");
        sb.AppendLine($"Osciladores: {Osciladores}, Monofónico");
        return sb.ToString();
    }
}

// Solina
public class Solina : Sintetizador
{
    public override int Osciladores => 1;
    public override int Polifonia => 49; // String ensemble
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 49;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 220;
    public override string TipoDeTension => "AC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: Solina String Ensemble");
        sb.AppendLine($"Polifonía: {Polifonia} notas");
        return sb.ToString();
    }
}

// Oddissey
public class Oddissey : Sintetizador
{
    public override int Osciladores => 2;
    public override int Polifonia => 2; // Duofónico
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 37;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: Oddissey (Duofónico)");
        sb.AppendLine($"Osciladores: {Osciladores}, Polifonía: {Polifonia}");
        return sb.ToString();
    }
}

PASO 3: Uso en Program.cs (ejemplo)
class Program
{
    static void Main()
    {
        var mono = new MonoPoly();
        var solina = new Solina();
        var arp = new ARP2600();

        Console.WriteLine(Sintetizador.Saludar());
        Console.WriteLine($"Sintes creados: {Sintetizador.CantidadDeSintes}\n");

        Console.WriteLine(mono.Encender());
        Console.WriteLine(mono.MostrarInfo());

        Console.WriteLine(solina.Encender());
        Console.WriteLine(solina.MostrarInfo());

        // Animación (solo si está encendido)
        // mono.GenerarOndaSenoAnimada(); // Descomenta para probar
    }
}


-------------------------------------------------------------------------------------------------------------------
Beneficios de esta herencia

Ventaja                               Explicación

No repites código                     Atributos y métodos comunes en la clase base

Cada modelo es un tipo                MonoPoly es un Sintetizador, puedes usar polimorfismo

Fácil agregar nuevos modelos          Solo creas una nueva clase

Sobrescritura clara                   MostrarInfo() se adapta a cada modelo

Mantenible y escalable                Ideal para crecer el proyecto



¿Qué pasa con el enum?

Ya no lo necesitas para definir modelos. Puedes eliminarlo o usarlo solo como índice si quieres una fábrica:

public static Sintetizador Crear(ModelosSintes modelo)
{
    return modelo switch
    {
        ModelosSintes.MonoPoly => new MonoPoly(),
        ModelosSintes.ARP2600 => new ARP2600(),
        ModelosSintes.Solina => new Solina(),
        _ => new MonoPoly()
    };
}


=====================================================================================================================

Fábrica

¡Perfecto! Vamos a **completar el proyecto** con:

1. **Fábrica de sintetizadores** (usando el `enum`)
2. **Menú interactivo en consola** (para elegir, encender, ver info, batería, animaciones)
3. **Clase `Program.cs` funcional**
4. **Animación de onda senoidal mejorada**
5. **Manejo de batería que baja con el uso**

---

## 1. Fábrica de Sintetizadores

```csharp
public static class FabricaSintetizadores
{
    public static Sintetizador Crear(ModelosSintes modelo)
    {
        return modelo switch
        {
            ModelosSintes.MonoPoly => new MonoPoly(),
            ModelosSintes.ARP2600 => new ARP2600(),
            ModelosSintes.Model_D => new ModelD(),
            ModelosSintes.Solina => new Solina(),
            ModelosSintes.Oddissey => new Oddissey(),
            ModelosSintes.MS_5 => new MS5(),
            ModelosSintes.MS_101 => new MS101(),
            ModelosSintes.Generico => new Generico(),
            _ => new MonoPoly()
        };
    }
}
```

---

## 2. Modelos faltantes (MS-5, MS-101, Generico)

```csharp
public class MS5 : Sintetizador
{
    public override int Osciladores => 1;
    public override int Polifonia => 1;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 25;
    public override bool TienePantalla => true;
    public override int TensionDeTrabajo => 9;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: MS-5 (Monofónico compacto)");
        sb.AppendLine($"Pantalla: Sí, Teclas: {NumeroDeTeclas}");
        return sb.ToString();
    }
}

public class MS101 : Sintetizador
{
    public override int Osciladores => 1;
    public override int Polifonia => 1;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 32;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 9;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: MS-101 (SH-101 clone)");
        sb.AppendLine($"Teclas: {NumeroDeTeclas}, Analógico puro");
        return sb.ToString();
    }
}

public class Generico : Sintetizador
{
    public override int Osciladores => 2;
    public override int Polifonia => 4;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 37;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo()
    {
        var sb = new StringBuilder(base.MostrarInfo());
        sb.AppendLine("Modelo: Genérico (Configurable)");
        return sb.ToString();
    }
}
```

---

## 3. `Program.cs` con Menú Interactivo

```csharp
using System.Text;

namespace sintetizador2
{
    class Program
    {
        private static List<Sintetizador> _sintes = new List<Sintetizador>();
        private static bool _ejecutando = true;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            MostrarBienvenida();

            while (_ejecutando)
            {
                MostrarMenu();
                ProcesarOpcion();
            }
        }

        static void MostrarBienvenida()
        {
            Console.Clear();
            Console.WriteLine(Sintetizador.Saludar());
            Console.WriteLine($"Fabricante: {Sintetizador.Fabricante} | Firmware: {Sintetizador.VersionFirmware}\n");
            Pausa();
        }

        static void MostrarMenu()
        {
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║            MENU PRINCIPAL - SINTETIZADORES       ║");
            Console.WriteLine("╠══════════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Crear nuevo sintetizador                      ║");
            Console.WriteLine("║ 2. Encender/Apagar sintetizador                  ║");
            Console.WriteLine("║ 3. Mostrar información de todos                  ║");
            Console.WriteLine("║ 4. Chequear batería                              ║");
            Console.WriteLine("║ 5. Generar onda senoidal animada                 ║");
            Console.WriteLine("║ 6. Simular uso (bajar batería)                   ║");
            Console.WriteLine("║ 7. Salir                                         ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");
        }

        static void ProcesarOpcion()
        {
            if (!int.TryParse(Console.ReadLine(), out int opcion))
            {
                MostrarError("Opción inválida.");
                return;
            }

            switch (opcion)
            {
                case 1: CrearSintetizador(); break;
                case 2: EncenderApagar(); break;
                case 3: MostrarTodos(); break;
                case 4: ChequearBaterias(); break;
                case 5: AnimarOnda(); break;
                case 6: SimularUso(); break;
                case 7: _ejecutando = false; Console.WriteLine("¡Hasta luego!"); break;
                default: MostrarError("Opción no válida."); break;
            }
        }

        static void CrearSintetizador()
        {
            Console.Clear();
            Console.WriteLine("CREAR NUEVO SINTETIZADOR\n");
            MostrarModelos();

            if (!int.TryParse(Console.ReadLine(), out int seleccion) || !Enum.IsDefined(typeof(ModelosSintes), seleccion))
            {
                MostrarError("Modelo inválido.");
                return;
            }

            var modelo = (ModelosSintes)seleccion;
            var sinte = FabricaSintetizadores.Crear(modelo);
            _sintes.Add(sinte);

            Console.WriteLine($"\n¡{modelo} creado correctamente!");
            Console.WriteLine($"Total de sintes: {Sintetizador.CantidadDeSintes}");
            Pausa();
        }

        static void MostrarModelos()
        {
            Console.WriteLine("Modelos disponibles:");
            foreach (var modelo in Enum.GetValues(typeof(ModelosSintes)))
            {
                Console.WriteLine($" {(int)modelo}. {modelo}");
            }
            Console.Write("\nElija un modelo: ");
        }

        static void EncenderApagar()
        {
            var sinte = SeleccionarSinte();
            if (sinte == null) return;

            Console.WriteLine($"\nEstado actual: {(sinte.Encendido ? "ON" : "OFF")}");
            Console.Write("¿Encender (e) / Apagar (a)?: ");
            var key = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (key == 'e' || key == 'E')
                Console.WriteLine(sinte.Encender());
            else if (key == 'a' || key == 'A')
                Console.WriteLine(sinte.Apagar());

            Pausa();
        }

        static void MostrarTodos()
        {
            Console.Clear();
            if (!_sintes.Any())
            {
                Console.WriteLine("No hay sintetizadores creados.");
                Pausa();
                return;
            }

            Console.WriteLine($"INFORMACIÓN DE {_sintes.Count} SINTETIZADOR(ES)\n");
            for (int i = 0; i < _sintes.Count; i++)
            {
                Console.WriteLine($"--- SINTE {i + 1} ---");
                Console.WriteLine(_sintes[i].MostrarInfo());
                Console.WriteLine();
            }
            Pausa();
        }

        static void ChequearBaterias()
        {
            Console.Clear();
            if (!_sintes.Any())
            {
                Console.WriteLine("No hay sintetizadores.");
                Pausa();
                return;
            }

            Console.WriteLine("ESTADO DE BATERÍAS\n");
            foreach (var s in _sintes)
            {
                Console.WriteLine($"{s.GetType().Name}: {s.NivelBateria}% → {s.EstadoBateriaPorcentaje}");
            }
            Pausa();
        }

        static void AnimarOnda()
        {
            var sinte = SeleccionarSinteEncendido();
            if (sinte == null) return;

            Console.Clear();
            Console.WriteLine($"Generando onda senoidal en {sinte.GetType().Name}...");
            Console.WriteLine("Presione cualquier tecla para detener.\n");

            Task.Run(() =>
            {
                GenerarOndaAnimadaConsola();
            });

            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Animación detenida.");
            Pausa();
        }

        static void SimularUso()
        {
            var sinte = SeleccionarSinteEncendido();
            if (sinte == null) return;

            Console.Write("¿Cuánto uso simular (1-100%)?: ");
            if (!int.TryParse(Console.ReadLine(), out int uso) || uso <= 0 || uso > 100)
            {
                MostrarError("Valor inválido.");
                return;
            }

            // Accedemos al campo protegido vía reflexión (o mejor: agregar método en base)
            // Alternativa limpia: agregar método en base
            typeof(Sintetizador).GetProperty("NivelBateria", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)?
                .GetSetMethod(true)?.Invoke(sinte, new object[] { sinte.NivelBateria - uso });

            Console.WriteLine($"Batería reducida en {uso}%. Nivel actual: {sinte.NivelBateria}%");
            Pausa();
        }

        static Sintetizador? SeleccionarSinte()
        {
            if (!_sintes.Any())
            {
                Console.WriteLine("No hay sintetizadores creados.");
                Pausa();
                return null;
            }

            Console.WriteLine("Seleccione un sintetizador:");
            for (int i = 0; i < _sintes.Count; i++)
            {
                var s = _sintes[i];
                Console.WriteLine($" {i + 1}. {s.GetType().Name} [{(s.Encendido ? "ON" : "OFF")}] - Batería: {s.NivelBateria}%");
            }
            Console.Write("Número: ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > _sintes.Count)
            {
                MostrarError("Índice inválido.");
                return null;
            }
            return _sintes[idx - 1];
        }

        static Sintetizador? SeleccionarSinteEncendido()
        {
            var sinte = SeleccionarSinte();
            if (sinte == null) return null;
            if (!sinte.Encendido)
            {
                Console.WriteLine("El sintetizador debe estar encendido para esta función.");
                Pausa();
                return null;
            }
            return sinte;
        }

        static void GenerarOndaAnimadaConsola()
        {
            int ancho = 80, alto = 20;
            double freq = 2 * Math.PI / 40;
            int desplazamiento = 0;

            while (true)
            {
                if (Console.KeyAvailable) break;

                Console.SetCursorPosition(0, 3);
                for (int y = 0; y < alto; y++)
                {
                    for (int x = 0; x < ancho; x++)
                    {
                        double valor = Math.Sin((x + desplazamiento) * freq);
                        int posY = (int)((valor + 1) * (alto - 1) / 2);
                        Console.Write(alto - y - 1 == posY ? "*" : " ");
                    }
                    Console.WriteLine();
                }
                desplazamiento++;
                Thread.Sleep(100);
            }
        }

        static void MostrarError(string msg)
        {
            Console.WriteLine($"\nERROR: {msg}");
            Pausa();
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
```

---

## 4. Mejora en `Sintetizador` (para batería)

Agrega este **método protegido** en la clase base para permitir bajar batería:

```csharp
protected void ReducirBateria(int cantidad)
{
    NivelBateria -= cantidad;
}
```

Y en `SimularUso()` cambia:

```csharp
// En lugar de reflexión, usa:
(sinte as dynamic)?.ReducirBateria?.Invoke(cantidad);
// O mejor: haz NivelBateria settable desde fábrica o agrega método público
```

**Solución limpia**: haz `NivelBateria` con `set` público (ya lo tiene), así que puedes hacer:

```csharp
sinte.NivelBateria -= uso; // Funciona porque tiene set
```

---

## Resultado Final

- **Herencia bien aplicada**
- **Fábrica + Menú interactivo**
- **Animación en tiempo real**
- **Batería dinámica**
- **Fácil de expandir**

---

¿Quieres que te genere un **.exe** o un **proyecto Visual Studio listo para compilar**? Puedo darte 
los archivos completos en ZIP o instrucciones paso a paso.


==============================================================================================================
Explicación del tipo virtual


El uso de `virtual` y `override` es clave en la herencia con polimorfismo, y te explico por qué 
lo usé en tu código y cuándo es necesario.



¿Qué significa `virtual`?


public virtual string MostrarInfo() { ... }


- `virtual` = "Este método puede ser sobrescrito en clases derivadas".
- Permite que una clase hija reemplace la implementación de la clase base.
- Es fundamental para el polimorfismo.



Ejemplo práctico: ¿Por qué `virtual`?

Supón que tienes:


Sintetizador sinte = new MonoPoly();
Console.WriteLine(sinte.MostrarInfo());


Sin `virtual` / `override`:


// En clase base
public string MostrarInfo() => "Sintetizador genérico";

// En MonoPoly
public string MostrarInfo() => "MonoPoly: 1 osc, 37 teclas"; // ¡NO se llama!


→ Resultado: Imprime "Sintetizador genérico" 
→ No hay polimorfismo → siempre se llama el de la clase base.



Con `virtual` / `override`:


// En clase base
public virtual string MostrarInfo() => "Sintetizador genérico";

// En MonoPoly
public override string MostrarInfo() => "MonoPoly: 1 osc, 37 teclas";
```

→ resultado**: Imprime "MonoPoly: 1 osc, 37 teclas"
→ ¡Sí hay polimorfismo! → se llama el método correcto según el objeto real.



¿Por qué usé `virtual` en estos métodos?

  Método                                      ¿Por qué `virtual`? 

`Encender()`                                  Algunos modelos podrían tener secuencia especial (luces, chequeo de firmware) 
`Apagar()`                                    Podría guardar presets, apagar pantalla, etc. 
`MostrarInfo()`                               ¡Cada modelo muestra info diferente! → obligatorio sobrescribir
`GenerarOndaSenoAnimada()`                    Un modelo podría tener forma de onda propia (ej: Solina con chorus)

---

¿Cuándo **NO** usar `virtual`?

- Métodos completamente comunes y que nunca cambiarán:
 
  public string Saludar() => "Bienvenido"; // No necesita override
  ```
- Métodos `private` o `static` → no se heredan.

---

Alternativa: `abstract` (aún más fuerte)

Si **todos** los modelos deben implementar un método:


public abstract string MostrarInfo(); // ¡Obliga a override!
```

→ Entonces cada clase hija **debe** tener:


public override string MostrarInfo() { ... }
```

### ¿Cuál usar?

| Usa `virtual` |                                               Usa `abstract` 

| Tienes una implementación **por defecto** útil |              No hay implementación por defecto
| Algunos modelos pueden usar la base |                         Todos deben personalizarlo

---

 Recomendación para tu código

Usa `virtual` + `override` cuando:
- Hay una **versión base útil**, pero **puede mejorarse**.
- Ejemplo: `MostrarInfo()`, `Encender()`, `GenerarOnda...`

Usa `abstract` cuando:
- **No tiene sentido** una versión genérica.
- Ejemplo: `Osciladores`, `Polifonia`, `TieneTeclas` → **son propiedades abstractas**

Ya las tienes bien como `abstract` → ¡perfecto!

---

## Resumen visual

```csharp
public abstract class Sintetizador
{
    public virtual string Encender() => "ON";           // Puede sobrescribirse
    public virtual string MostrarInfo() => "Genérico";  // ¡Debe sobrescribirse!
    public abstract int Osciladores { get; }           // ¡Obliga a implementar!
}
```

```csharp
public class MonoPoly : Sintetizador
{
    public override string MostrarInfo() => "MonoPoly: 1 osc"; // ¡Se llama!
    public override int Osciladores => 1;                     // ¡Obligatorio!
}
```

---

## Conclusión

> **`virtual` permite que cada sintetizador "sea único" aunque lo trates como `Sintetizador`**

Sin `virtual` → **pierdes el poder de la herencia**.  
Con `virtual` → **cada modelo brilla con su personalidad**.

---

¿Quieres que te muestre cómo se vería **sin `virtual`** para que veas el error en vivo? Puedo hacerte un ejemplo que falla.

=========================================================================================================================
=========================================================================================================================


Sobrecargas

¡Buena pregunta! Vamos a analizar si hace falta poner sobrecargas** (en C#, hablamos de sobrecarga de métodos o 
constructores) en tu clase `Sintetizador` y sus derivadas, y te daré una respuesta clara, práctica y orientada a 
buen diseño.



Respuesta corta:
> No hacen falta sobrecargas en esta clase tal como está diseñada.
> Pero sí podrías beneficiarte de sobrecargas en el futuro si quieres más flexibilidad (por ejemplo, inicializar con 
batería baja, o encender/apagar con mensajes personalizados).



Análisis detallado

1. ¿Qué es una sobrecarga?
Sobrecarga = tener **varios métodos con el mismo nombre pero diferentes parámetros.

Ejemplo:

public string Encender()
public string Encender(string mensaje)




2. ¿Tienes sobrecargas ahora?
No. 
Todos tus métodos tienen una única firma:

csharp
public virtual string Encender()
public virtual string Apagar()
public virtual string MostrarInfo()


Y el constructor es único:
csharp
protected Sintetizador() { ... }




3. ¿Es obligatorio tener sobrecargas?**
No.  
El código funciona perfectamente sin ellas.

Pero podrías mejorar la usabilidad y flexibilidad con sobrecargas bien pensadas.

---

¿Dónde SÍ podrías usar sobrecargas útilmente?

1. Constructor con parámetros

protected Sintetizador(int nivelBateriaInicial = 100)
{
    _encendido = false;
    _nivelBateria = nivelBateriaInicial < 0 ? 0 : nivelBateriaInicial > 100 ? 100 : nivelBateriaInicial;
    _cantidadDeSintes++;
}


Beneficio: Puedes crear sintetizadores con batería baja para pruebas:

var sinBateria = new MonoPoly(10);




2. Sobrecarga de `Encender()`

public virtual string Encender()
    => Encender("Encendido por defecto");

public virtual string Encender(string mensaje)
{
    _encendido = true;
    return $"{GetType().Name}: {mensaje}";
}
```

**Beneficio:** Mensajes personalizados:

Console.WriteLine(mono.Encender("¡Listo para tocar!"));
```

---

3. Sobrecarga de `MostrarInfo()`

public virtual string MostrarInfo(bool incluirTecnico = false)
{
    var sb = new StringBuilder(base.MostrarInfo());
    
    if (incluirTecnico)
    {
        sb.AppendLine($"Fabricante: {Fabricante}, Firmware: {VersionFirmware}");
        sb.AppendLine($"Tensión: {TensionDeTrabajo}V {TipoDeTension}");
    }
    
    return sb.ToString();
}


Beneficio: Información básica o técnica según contexto.



4. Métodos de onda con parámetros

public virtual void GenerarOndaSenoAnimada(int duracionSegundos = 3)
{
    if (!_encendido) return;
    // animación durante X segundos
}
```

---

¿Y en las clases derivadas?

Puedes sobrescribir y luego sobrecargar en clases específicas si algún modelo tiene comportamiento especial.

Ejemplo en `Solina`:

public override void GenerarOndaSenoAnimada()
    => GenerarOndaSenoAnimada("cuerdas vibrantes");

public void GenerarOndaSenoAnimada(string estilo)
{
    if (!_encendido) return;
    Console.WriteLine($"Solina: onda senoidal estilo {estilo}...");
}




Conclusión: ¿Hace falta?

| Pregunta | Respuesta |
|--------|----------|
| **¿Funciona sin sobrecargas?** | Sí, perfectamente |
| **¿Mejora el diseño con sobrecargas?** | Sí, mucho (flexibilidad, pruebas, UX) |
| **¿Es obligatorio?** | No |
| **¿Recomendable?** | **Sí, al menos en el constructor y `Encender()`** |

---

## Recomendación final (código mejorado)

### En `Sintetizador` (base):

```csharp
protected Sintetizador(int nivelBateriaInicial = 100)
{
    _encendido = false;
    _nivelBateria = nivelBateriaInicial < 0 ? 0 : nivelBateriaInicial > 100 ? 100 : nivelBateriaInicial;
    _cantidadDeSintes++;
}

public virtual string Encender() => Encender("ON");
public virtual string Encender(string mensaje)
{
    _encendido = true;
    return $"{GetType().Name}: {mensaje}";
}
```

### En `Program.cs`:
```csharp
var mono = new MonoPoly(20); // ¡Con 20% de batería!
Console.WriteLine(mono.Encender("¡Arranque de emergencia!"));
```

---

## Resumen

> **No hacen falta sobrecargas para que funcione.**  
> **Pero SÍ las recomiendo** para:
> - Constructores con batería inicial
> - `Encender()` con mensaje
> - `MostrarInfo()` con modo técnico
> - Métodos de onda con duración/estilo

**Tu código está muy bien estructurado.** Con unas pocas sobrecargas, será **más profesional y flexible**.

--------------------------------------------------------------------------------------------------------------------


¡Perfecto! Aquí tienes todo el código mejorado con sobrecargas útiles, bien diseñadas y prácticas, manteniendo
tu estructura original pero elevándola a un nivel más profesional, flexible y extensible.



## OBJETIVOS DE LAS SOBRECARGAS AÑADIDAS

| Método | Sobrecarga añadida | Beneficio |
|-------|---------------------|---------|
| `Sintetizador()` | `nivelBateriaInicial` | Crear sintetizadores con batería personalizada |
| `Encender()` | Con mensaje | Mensajes personalizados |
| `Apagar()` | Con mensaje | Mensajes personalizados |
| `MostrarInfo()` | `incluirTecnico` | Mostrar info técnica opcional |
| `GenerarOndaSenoAnimada()` | `duracionSegundos` | Controlar tiempo de animación |

---

CÓDIGO COMPLETO MEJORADO

---

PASO 1: Clase base `Sintetizador` con sobrecargas


using System.Text;

namespace sintetizador2
{
    public abstract class Sintetizador
    {
        // Atributos comunes
        protected bool _encendido;
        protected int _nivelBateria;
        protected static int _cantidadDeSintes = 0;
        private static readonly string _fabricante = "Behringer";
        private static readonly string _versionFirmware = "version 1.0.0";

        // === CONSTRUCTORES CON SOBRECARGA ===
        protected Sintetizador() : this(100) { }

        protected Sintetizador(int nivelBateriaInicial)
        {
            _encendido = false;
            NivelBateria = nivelBateriaInicial; // Usa el setter con validación
            _cantidadDeSintes++;
        }

        // === PROPIEDADES ESTÁTICAS ===
        public static int CantidadDeSintes => _cantidadDeSintes;
        public static string Fabricante => _fabricante;
        public static string VersionFirmware => _versionFirmware;

        // === PROPIEDADES DE INSTANCIA ===
        public bool Encendido => _encendido;

        public int NivelBateria
        {
            get => _nivelBateria;
            set => _nivelBateria = value < 0 ? 0 : value > 100 ? 100 : value;
        }

        public string EstadoBateriaPorcentaje
        {
            get
            {
                if (NivelBateria == 100) return "Batería completa";
                if (NivelBateria >= 66) return "Batería: OK +";
                if (NivelBateria >= 33) return "Batería: OK -";
                return "Recargar batería";
            }
        }

        // === MÉTODOS CON SOBRECARGA ===

        // Encender
        public virtual string Encender() => Encender("ON");

        public virtual string Encender(string mensaje)
        {
            if (_encendido) return $"{GetType().Name}: Ya estaba encendido";
            _encendido = true;
            return $"{GetType().Name}: {mensaje}";
        }

        // Apagar
        public virtual string Apagar() => Apagar("OFF");

        public virtual string Apagar(string mensaje)
        {
            if (!_encendido) return $"{GetType().Name}: Ya estaba apagado";
           	rs _encendido = false;
            return $"{GetType().Name}: {mensaje}";
        }

        // MostrarInfo con opción técnica
        public virtual string MostrarInfo() => MostrarInfo(false);

        public virtual string MostrarInfo(bool incluirTecnico)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{GetType().Name}: {(Encendido ? "ON" : "OFF")}");
            sb.AppendLine($"Batería: {NivelBateria}% - {EstadoBateriaPorcentaje}");

            if (incluirTecnico)
            {
                sb.AppendLine($"Fabricante: {Fabricante} | Firmware: {VersionFirmware}");
                sb.AppendLine($"Tensión: {TensionDeTrabajo}V {TipoDeTension}");
            }

            return sb.ToString();
        }

        // === PROPIEDADES ABSTRACTAS ===
        public abstract int Osciladores { get; }
        public abstract int Polifonia { get; }
        public abstract bool TieneTeclas { get; }
        public abstract int NumeroDeTeclas { get; }
        public abstract bool TienePantalla { get; }
        public abstract int TensionDeTrabajo { get; }
        public abstract string TipoDeTension { get; }

        // === MÉTODOS DE ONDA CON SOBRECARGA ===
        public virtual void GenerarOndaSenoAnimada() => GenerarOndaSenoAnimada(3);

        public virtual void GenerarOndaSenoAnimada(int duracionSegundos)
        {
            if (!_encendido)
            {
                Console.WriteLine("Error: Sintetizador apagado.");
                return;
            }

            Console.Clear();
            Console.WriteLine($"Generando onda senoidal genérica ({duracionSegundos}s)...");

            for (int i = 0; i < duracionSegundos * 10; i++)
            {
                Console.CursorLeft = 0;
                double fase = i * 0.3;
                int amplitud = 20;
                int onda = (int)(Math.Sin(fase) * amplitud) + amplitud + 5;
                Console.Write(new string(' ', onda) + "█");
                Thread.Sleep(100);
            }
            Console.WriteLine("\n¡Onda completada!");
        }

        public virtual void GenerarOndaTriangular()
        {
            if (!_encendido)
            {
                Console.WriteLine("Error: Sintetizador apagado.");
                return;
            }
            Console.WriteLine("Onda triangular no implementada en este modelo.");
        }
    }
}
```

---

### **PASO 2: Clases derivadas (con mejoras)**

```csharp
// MonoPoly
public class MonoPoly : Sintetizador
{
    public MonoPoly(int bateria = 100) : base(bateria) { }

    public override int Osciladores => 1;
    public override int Polifonia => 1;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 37;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo(bool incluirTecnico = false)
    {
        var sb = new StringBuilder(base.MostrarInfo(incluirTecnico));
        sb.AppendLine("Modelo: MonoPoly (Monofónico)");
        sb.AppendLine($"Osciladores: {Osciladores}, Polifonía: {Polifonia}");
        if (incluirTecnico) sb.AppendLine($"Teclas: {NumeroDeTeclas}");
        return sb.ToString();
    }
}

// ARP2600
public class ARP2600 : Sintetizador
{
    public ARP2600(int bateria = 100) : base(bateria) { }

    public override int Osciladores => 2;
    public override int Polifonia => 1;
    public override bool TieneTeclas => false;
    public override int NumeroDeTeclas => 0;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 15;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo(bool incluirTecnico = false)
    {
        var sb = new StringBuilder(base.MostrarInfo(incluirTecnico));
        sb.AppendLine("Modelo: ARP2600 (Semi-modular)");
        sb.AppendLine($"Osciladores: {Osciladores}, Duofónico limitado");
        return sb.ToString();
    }
}

// Model D
public class ModelD : Sintetizador
{
    public ModelD(int bateria = 100) : base(bateria) { }

    public override int Osciladores => 3;
    public override int Polifonia => 1;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 0;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo(bool incluirTecnico = false)
    {
        var sb = new StringBuilder(base.MostrarInfo(incluirTecnico));
        sb.AppendLine("Modelo: Model D (Minimoog clone)");
        sb.AppendLine($"Osciladores: {Osciladores}, Monofónico");
        return sb.ToString();
    }
}

// Solina
public class Solina : Sintetizador
{
    public Solina(int bateria = 100) : base(bateria) { }

    public override int Osciladores => 1;
    public override int Polifonia => 49;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 49;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 220;
    public override string TipoDeTension => "AC";

    public override string MostrarInfo(bool incluirTecnico = false)
    {
        var sb = new StringBuilder(base.MostrarInfo(incluirTecnico));
        sb.AppendLine("Modelo: Solina String Ensemble");
        sb.AppendLine($"Polifonía: {Polifonia} notas");
        return sb.ToString();
    }

    public override void GenerarOndaSenoAnimada(int duracionSegundos)
    {
        if (!_encendido) { Console.WriteLine("Error: Solina apagada."); return; }
        Console.Clear();
        Console.WriteLine($"Solina: cuerdas vibrantes ({duracionSegundos}s)...");
        // Animación especial
        for (int i = 0; i < duracionSegundos * 8; i++)
        {
            Console.CursorLeft = 0;
            string onda = i % 2 == 0 ? "♪ ♫ ♪" : "♫ ♪ ♫";
            Console.Write(onda.PadLeft(20 + i % 10));
            Thread.Sleep(125);
        }
        Console.WriteLine("\n¡Ensemble de cuerdas listo!");
    }
}

// Oddissey
public class Oddissey : Sintetizador
{
    public Oddissey(int bateria = 100) : base(bateria) { }

    public override int Osciladores => 2;
    public override int Polifonia => 2;
    public override bool TieneTeclas => true;
    public override int NumeroDeTeclas => 37;
    public override bool TienePantalla => false;
    public override int TensionDeTrabajo => 12;
    public override string TipoDeTension => "DC";

    public override string MostrarInfo(bool incluirTecnico = false)
    {
        var sb = new StringBuilder(base.MostrarInfo(incluirTecnico));
        sb.AppendLine("Modelo: Oddissey (Duofónico)");
        sb.AppendLine($"Osciladores: {Osciladores}, Polifonía: {Polifonia}");
        return sb.ToString();
    }
}
```

---

### **PASO 3: `Program.cs` con ejemplos de uso**

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var mono = new MonoPoly(25);     // ¡Batería baja!
        var solina = new Solina();
        var arp = new ARP2600(80);

        Console.WriteLine($"Sintes creados: {Sintetizador.CantidadDeSintes}\n");

        // Uso con sobrecargas
        Console.WriteLine(mono.Encender("¡Arranque de emergencia!"));
        Console.WriteLine(mono.MostrarInfo(incluirTecnico: true));

        Console.WriteLine(solina.Encender());
        Console.WriteLine(solina.MostrarInfo());

        // Animaciones
        mono.GenerarOndaSenoAnimada(duracionSegundos: 2);
        solina.GenerarOndaSenoAnimada(4);

        // Apagar con mensaje
        Console.WriteLine(mono.Apagar("Modo ahorro activado"));

        Console.WriteLine($"\nSintes totales: {Sintetizador.CantidadDeSintes}");
        Console.WriteLine($"Fabricante: {Sintetizador.Fabricante} | FW: {Sintetizador.VersionFirmware}");
    }
}
```

---

## RESULTADO FINAL

- **Sobrecargas bien aplicadas**  
- **Constructores flexibles**  
- **Mensajes personalizados**  
- **Animaciones controladas**  
- **Información técnica opcional**  
- **Código más profesional y reutilizable**

---

¿Quieres que ahora agreguemos **eventos** (ej: `BateriaBaja`), **excepciones**, o **una interfaz de usuario en consola**?

 */
