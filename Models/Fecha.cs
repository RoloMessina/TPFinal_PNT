namespace TPFinal_PNT1.Models
{
    public class Fecha
    {
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public double Hora { get; set; }

        public Fecha(int dia, int mes, int anio, double hora)
        {
            Dia = dia;
            Mes = mes;
            Anio = anio;
            Hora = hora;
        }

        public static Fecha Hoy()
        {
            var hoy = DateTime.Now;
            return new Fecha(hoy.Day, hoy.Month, hoy.Year, hoy.TimeOfDay.TotalHours);
        }
    }
}
