namespace TCC.Models
{
    public class MetodosInicioModel
    {   
        public String status(string status){
        String bolinha = "";
        if(status.Equals("Pago")){
            bolinha = "bolinhaVerde";
        }
        else if(status.Equals("Pendente")){
            bolinha = "bolinhaAmarela";
        }
        else if(status.Equals("Atrasado")){
            bolinha = "bolinhaVermelha";
        }
        return bolinha;
        }
    }
}