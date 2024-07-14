using ControlePedido.Application.DTOs;
using ControlePedido.Domain.Adapters.Providers;
using ControlePedido.Domain.Adapters.Repositories;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Entities;
using static ControlePedido.Domain.Entities.Pedido;

namespace ControlePedido.Application.UseCases.Pedidos
{
    public class CriarPedidoUseCase : ICriarPedidoUseCase
    {
        private readonly IPedidoRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPagamentoProvider _pagamentoProvider;

        public CriarPedidoUseCase(IPedidoRepository repository,
                                  IPagamentoProvider pagamentoProvider,
                                  IProdutoRepository produtoRepository,
                                  IClienteRepository clienteRepository)
        {
            _repository = repository;
            _pagamentoProvider = pagamentoProvider;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<PedidoCriadoDTO> Executar(CriarPedidoDTO criarPedidoDTO)
        {
            Cliente? cliente = await ConsultarCliente(criarPedidoDTO.CpfCliente);

            var itensPedido = new List<PedidoItem>();

            foreach (var itens in criarPedidoDTO.Itens)
            {
                var produto = await _produtoRepository.ConsultarPorId(itens.ProdutoId);

                if (produto is null)
                    throw new DomainException($"Não foi encontrado um produto com id {itens.ProdutoId}");

                itensPedido.Add(new PedidoItem(produto));
            };

            var pedido = PedidoFactory.Criar(itensPedido, cliente);

            await SalvarPedido(pedido);

            var qrCode = _pagamentoProvider.GerarQRCodePagamento(pedido);

            return new PedidoCriadoDTO(pedido, qrCode);
        }

        private async Task<Cliente?> ConsultarCliente(string cpfCliente)
        {
            if (string.IsNullOrEmpty(cpfCliente))
                return null;

            var cliente = await _clienteRepository.ConsultarPorCpf(cpfCliente);

            if (cliente is null)
                throw new DomainException("Não foi localizado um cliente para o id informado!");

            return cliente;
        }

        private async Task SalvarPedido(Pedido pedido)
        {
            _repository.Criar(pedido);
            await _repository.UnitOfWork.Commit();
        }
    }
}

