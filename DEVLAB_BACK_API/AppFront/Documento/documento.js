async function enviarDocumento(){
	const codigoCliente = document.getElementById("codigoCliente").value;
	const inputArquivo = document.getElementById("arquivo");
	const arquivo = inputArquivo.files[0];

	alert("O codigo do cliente é: " + codigoCliente);

	if (!codigoCliente || !arquivo){
		alert("Informe o codigo do cliente e selecione um arquivo")
		return;
	}
	const dadosArquivo = new FormData();
	dadosArquivo.append("arquivo", arquivo);

	const response = await fetch(`${URL_API}/upload/${codigoCliente}`,{
		method:"POST",
		body: dadosArquivo
	});

	if (response.ok){
		alert("Documento enviado com sucesso!");
		document.getElementById("codigoCliente").value = "";
		document.getElementById("arquivo").value = "";
	}
}