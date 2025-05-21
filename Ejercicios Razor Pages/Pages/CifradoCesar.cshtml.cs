using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

public class CifradoCesarModel : PageModel
{
    [BindProperty] public string Mensaje { get; set; } = string.Empty;
    [BindProperty] public int Desplazamiento { get; set; } = 3; 
    [BindProperty] public string Accion { get; set; } = "codificar";

    public string Resultado { get; private set; } = string.Empty;
    public bool Procesado => !string.IsNullOrEmpty(Resultado);

    public void OnGet() { }

    public void OnPost()
    {
        if (Desplazamiento <= 0)
        {
            ModelState.AddModelError(nameof(Desplazamiento), "Debe ser mayor que cero.");
            return;
        }

        string texto = Mensaje.ToUpperInvariant();
        int n = Desplazamiento % 26;  

        StringBuilder sb = new();

        for (int i = 0; i < texto.Length; i++)
        {
            char c = texto[i];

            if (c >= 'A' && c <= 'Z')
            {
                int pos = c - 'A';
                int nuevaPos;

                if (Accion == "codificar")
                    nuevaPos = (pos + n) % 26;          
                else 
                    nuevaPos = (pos - n + 26) % 26;      

                sb.Append((char)('A' + nuevaPos));
            }
            else
            {
                sb.Append(c);
            }
        }

        Resultado = sb.ToString();
    }
}
