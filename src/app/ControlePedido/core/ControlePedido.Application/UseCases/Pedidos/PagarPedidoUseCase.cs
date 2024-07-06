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
            var pedido = await _repository.ConsultarPorId(pagarPedido.PedidoId);

            //TODO - colocar validacao pra verificar se pedido ja esta pago.

            if (pedido is null)
                throw new DomainException("Não foi encontrado um pedido com o código informado!");

            var codigoTranscacaoValido = await _pagamentoProvider.ValidarTransacao(pagarPedido.CodigoTransacao);

            if (!codigoTranscacaoValido)
                throw new DomainException("O código de transação informado não está válido!");

            pedido.Pagar(pagarPedido.CodigoTransacao);

            _repository.Atualizar(pedido);

            await _repository.UnitOfWork.Commit();
        }
    }
}

