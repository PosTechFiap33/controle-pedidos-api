using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;

namespace ControlePedido.Application.UseCases.Pedidos;

public class PagarPedidoManualmenteUseCase : IPagarPedidoManualmenteUseCase
{
    private readonly IPedidoRepository _repository;

    public PagarPedidoManualmenteUseCase(IPedidoRepository repository)
    {
        _repository = repository;
    }

    public async Task Executar(PagarPedidoManualDTO pagarPedido)
    {
        if(Guid.Empty == pagarPedido.PedidoId)
            throw new DomainException("O código do pedido não foi informado!");

        var pedido = await _repository.ConsultarPorId(pagarPedido.PedidoId);

        if (pedido is null)
            throw new DomainException("Não foi encontrado um pedido com o código informado!");

        pedido.Pagar(pagarPedido.CodigoTransacao, DateTime.Now, pagarPedido.ValorPago);

        _repository.Atualizar(pedido);

        await _repository.UnitOfWork.Commit();
    }
}
