using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Adapters.Services;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;

namespace ControlePedido.Application.UseCases.Pedidos
{
	public interface ICriarPedidoUseCase
	{
		Task<string> Executar(CriarPedidoDTO criarPedidoDTO);
	}

    public class CriarPedidoUseCase : ICriarPedidoUseCase
	{
		private readonly IPedidoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
		private readonly IPagamentoService _service;

        public CriarPedidoUseCase(IPedidoRepository repository,
                                  IPagamentoService service,
                                  IProdutoRepository produtoRepository,
                                  IClienteRepository clienteRepository)
        {
            _repository = repository;
            _service = service;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<string> Executar(CriarPedidoDTO criarPedidoDTO)
        {
            Pedido pedido;

            var clienteId = criarPedidoDTO.ClienteId;

            var itensPedido = criarPedidoDTO.Itens.Select(p => new PedidoItem(p.ProdutoId)).ToList();

            foreach(var itens in itensPedido)
            {
                var produto = await _produtoRepository.ConsultarPorId(itens.ProdutoId);

                if (produto is null)
                    throw new DomainException($"Não foi encontrado um pedido com id {itens.ProdutoId}");
            };

            if (!clienteId.HasValue || clienteId == Guid.Empty)
                pedido = new Pedido(itensPedido);
            else
            {
                var cliente = await _clienteRepository.ConsultarPorId(clienteId.Value);

                if (cliente is null)
                    throw new DomainException("Não foi localizado um cliente para o id informado!");

                pedido = new Pedido(itensPedido, cliente.Id);
            }

            _repository.Criar(pedido);

            await _repository.UnitOfWork.Commit();

            return _service.GerarQRCodePagamento(pedido);
        }
    }
}

