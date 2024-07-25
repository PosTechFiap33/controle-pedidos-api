namespace ControlePedido.Payment;

public class MercadoPagoIntegration
{
    public string Token { get; set; }
    public string UrlWebhook { get; set; }
    public long UserId { get; set; }
    public string ExternalPosId { get; set; }

    public MercadoPagoIntegration()
    {
        Token = GetEnvironmentVariableOrDefault("MERCADO_PAGO_TOKEN", Token);
        UrlWebhook = GetEnvironmentVariableOrDefault("MERCADO_PAGO_URL_WEBHOOK", UrlWebhook);
        UserId = long.Parse(GetEnvironmentVariableOrDefault("MERCADO_PAGO_USERID", UserId.ToString()));
        ExternalPosId = GetEnvironmentVariableOrDefault("MERCADO_PAGO_POSID", ExternalPosId);
    }

    private string GetEnvironmentVariableOrDefault(string variableName, string defaultValue = null)
    {
        string value = Environment.GetEnvironmentVariable(variableName);
        return !string.IsNullOrEmpty(value) ? value : defaultValue;
    }
}
