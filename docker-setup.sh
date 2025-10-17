#!/bin/bash

# Script para build e execução do Painel de Acompanhamento
# Autor: GitHub Copilot
# Data: $(date)

set -e

echo "🐳 Painel de Acompanhamento - Docker Setup"
echo "=========================================="

# Verificar se Docker está instalado
if ! command -v docker &> /dev/null; then
    echo "❌ Docker não está instalado. Instale o Docker primeiro."
    exit 1
fi

# Verificar se Docker Compose está instalado
if ! command -v docker-compose &> /dev/null; then
    echo "❌ Docker Compose não está instalado. Instale o Docker Compose primeiro."
    exit 1
fi

# Função para mostrar ajuda
show_help() {
    echo "Uso: ./docker-setup.sh [OPÇÃO]"
    echo ""
    echo "Opções:"
    echo "  build     Fazer build da imagem Docker"
    echo "  up        Subir os serviços (build + run)"
    echo "  down      Parar os serviços"
    echo "  restart   Reiniciar os serviços"
    echo "  logs      Mostrar logs da aplicação"
    echo "  clean     Limpar imagens e containers"
    echo "  help      Mostrar esta ajuda"
    echo ""
    echo "Exemplos:"
    echo "  ./docker-setup.sh up     # Subir a aplicação"
    echo "  ./docker-setup.sh logs   # Ver logs"
    echo "  ./docker-setup.sh down   # Parar aplicação"
}

# Função para build
docker_build() {
    echo "🔨 Fazendo build da aplicação..."
    docker-compose build --no-cache
    echo "✅ Build concluído!"
}

# Função para subir os serviços
docker_up() {
    echo "🚀 Subindo os serviços..."
    docker-compose up -d --build
    echo ""
    echo "✅ Aplicação rodando!"
    echo "🌐 Acesse: http://localhost:5798"
    echo "📊 Dashboard: http://localhost:5798/"
    echo "📋 Acompanhamento: http://localhost:5798/acompanhamento"
    echo ""
    echo "Para ver os logs: ./docker-setup.sh logs"
    echo "Para parar: ./docker-setup.sh down"
}

# Função para parar os serviços
docker_down() {
    echo "🛑 Parando os serviços..."
    docker-compose down
    echo "✅ Serviços parados!"
}

# Função para reiniciar
docker_restart() {
    echo "🔄 Reiniciando os serviços..."
    docker-compose down
    docker-compose up -d --build
    echo "✅ Serviços reiniciados!"
    echo "🌐 Acesse: http://localhost:5798"
}

# Função para mostrar logs
docker_logs() {
    echo "📄 Mostrando logs da aplicação..."
    docker-compose logs -f painel-web
}

# Função para limpeza
docker_clean() {
    echo "🧹 Limpando containers e imagens..."
    docker-compose down --rmi all --volumes --remove-orphans
    docker system prune -f
    echo "✅ Limpeza concluída!"
}

# Verificar argumentos
case "${1:-}" in
    "build")
        docker_build
        ;;
    "up")
        docker_up
        ;;
    "down")
        docker_down
        ;;
    "restart")
        docker_restart
        ;;
    "logs")
        docker_logs
        ;;
    "clean")
        docker_clean
        ;;
    "help"|"-h"|"--help")
        show_help
        ;;
    "")
        echo "❓ Nenhuma opção fornecida. Use 'help' para ver as opções disponíveis."
        show_help
        exit 1
        ;;
    *)
        echo "❌ Opção inválida: $1"
        show_help
        exit 1
        ;;
esac