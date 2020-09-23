using System;

namespace Dapper.Application.DTOs.RequestModels
{
    public class Asignacion_PlandePago
    {
    public int IdAsignacion {get; set;}
	public string Proyecto {get; set;}
	public string NumeroLote {get; set;}
	public decimal AreaLote {get; set;}
	public decimal Total {get; set;}
	public decimal Prima {get; set;}
	public string Beneficiario {get; set;}
	public int NumeroCuota {get; set;}
	public DateTime FechaCuota {get; set;}
	public string MesPagado {get; set;}
	public decimal SaldoInicial {get; set;}
	public decimal MontoMinimo {get; set;}
	public decimal Saldo {get; set;}
	public decimal Interes {get; set;} = 0;
	public decimal TotalPagar {get; set;}
	public decimal MontoPagado {get; set;}
	public string FechaPago	{get; set;}
	public string Estado {get; set;}
	public decimal Mora {get; set;} = 0;
	public int DiasMora {get; set;}= 0;
    }
}
