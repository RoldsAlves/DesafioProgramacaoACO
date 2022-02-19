function formataCPF(cpf) {
    const cpfFormatado = cpf;
    const cpfAtual = cpf.value;
    let cpfAtualizado;
    cpfAtualizado = cpfAtual.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/,
        function (regex, argumento1, argumento2, argumento3, argumento4) {
            return argumento1 + '.' + argumento2 + '.' + argumento3 + '-' + argumento4;
        });
    cpfFormatado.value = cpfAtualizado;
}