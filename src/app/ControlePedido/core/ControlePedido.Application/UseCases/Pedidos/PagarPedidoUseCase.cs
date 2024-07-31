using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public class PagarPedidoUseCase : IPagarPedidoUseCase
    {
        private readonly IPedidoRepository _repository;
        private readonly IPagamentoProvider _pagamentoProvider;

        public PagarPedidoUseCase(IPedidoRepository repository,
                                  IPagamentoProvider pagamentoProvider)
        {
            _repository = repository;
            _pagamentoProvider = pagamentoProvider;
        }

        public async Task Executar(PagarPedidoDTO pagarPedido)
        {
            var pagamentoRealizado = await _pagamentoProvider.ValidarTransacao(pagarPedido.CodigoTransacao);

            if (pagamentoRealizado == null)
                throw new DomainException("Não foi encontrado um pagamento para o código de transação informado!");

            var pedido = await _repository.ConsultarPorId(pagamentoRealizado.PedidoId);

            if (pedido is null)
                throw new DomainException("Não foi encontrado um pedido com o código informado!");

            pedido.Pagar(pagarPedido.CodigoTransacao, pagamentoRealizado.DataPagamento, pagamentoRealizado.ValorPago);

            _repository.Atualizar(pedido);

            await _repository.UnitOfWork.Commit();
        }
    }
}

