using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ImcModel : PageModel
{
    [BindProperty] public double Peso { get; set; }     
    [BindProperty] public double Altura { get; set; }   

    public double Imc { get; private set; }
    public string Clasificacion { get; private set; } = string.Empty;
    public string Imagen { get; private set; } = string.Empty;

    public bool Calculado => Imc > 0;

    public void OnGet() { }          

    public void OnPost()
    {
        if (Altura <= 0 || Peso <= 0)
        {
            ModelState.AddModelError(string.Empty, "Peso y altura deben ser mayores a cero.");
            return;
        }

        Imc = Peso / (Altura * Altura);

        switch (Imc)
        {
            case < 18:
                SetResultado("Peso Bajo", "bajo.png");
                break;

            case >= 18 and < 25:
                SetResultado("Peso Normal", "normal.png");
                break;

            case >= 25 and < 27:
                SetResultado("Sobrepeso", "sobrepeso.png");
                break;

            case >= 27 and < 30:
                SetResultado("Obesidad grado I", "obI.png");
                break;

            case >= 30 and < 40:
                SetResultado("Obesidad grado II", "obII.png");
                break;

            default:
                SetResultado("Obesidad grado III", "obIII.png");
                break;
        }
    }

    private void SetResultado(string clasif, string img)
    {
        Clasificacion = clasif;
        Imagen = img;              
    }
}
