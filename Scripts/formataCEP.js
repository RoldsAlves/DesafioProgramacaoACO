function formataCEP(cep) {
    const cepFormatado = cep;
    const cepAtual = cep.value;
    let cepAtualizado;
    cepAtualizado = cepAtual.replace(/(\d{5})(\d{3})/,
        function (regex, argumento1, argumento2) {
            return argumento1 + '-' + argumento2;
        });
    cepFormatado.value = cepAtualizado;
}