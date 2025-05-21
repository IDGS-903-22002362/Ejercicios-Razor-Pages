using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EstadisticaModel : PageModel
{
    public int[] Numeros { get; private set; } = Array.Empty<int>();
    public int[] Ordenados { get; private set; } = Array.Empty<int>();
    public int Suma { get; private set; }
    public double Promedio { get; private set; }
    public List<int> Moda { get; private set; } = new();
    public double Mediana { get; private set; }
    public bool Generado => Numeros.Length > 0;

    public void OnGet() { }

    public void OnPost()
    {
        Random rnd = new();
        Numeros = Enumerable.Range(0, 20)
                            .Select(_ => rnd.Next(0, 101))   
                            .ToArray();

        Suma = Numeros.Sum();
        Promedio = Suma / 20.0;

        Ordenados = Numeros.OrderBy(n => n).ToArray();

        int mid1 = Ordenados[9];
        int mid2 = Ordenados[10];
        Mediana = (mid1 + mid2) / 2.0;

        var grupos = Numeros.GroupBy(n => n)
                            .Select(g => new { Valor = g.Key, Frec = g.Count() });
        int maxFrec = grupos.Max(g => g.Frec);
        Moda = grupos.Where(g => g.Frec == maxFrec)
                     .Select(g => g.Valor)
                     .OrderBy(v => v)
                     .ToList();
    }
}
