namespace Dapper.Application.DTOs.RequestModels
{
  public class EstadoCuenta
  {
    public int IdAsignacion { get; set; }
    public string NumeroLote { get; set; }
    public string Area { get; set; }
    public string Proyecto { get; set; }
    public decimal ValorTotal { get; set; }
    public string FechaInicioPago { get; set; }
    public string Beneficiario { get; set; }

    public int? NumeroCuota { get; set; }
    public string NumeroRecibo { get; set; }
    public string FechaRecibo { get; set; }
    public string MesPagado { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal MontoCuota { get; set; }
    public decimal Interes { get; set; }
    public decimal? DiasMora { get; set; } = 0;
    public decimal Mora { get; set; }
    public decimal TotalPago { get; set; }
    public decimal Saldo { get; set; }

    public string Empresa { get; set; } = "";
    public string DireccionEmpresa { get; set; } = "";
    public string TelefonoEmpresa { get; set; } = "";

  }
}
