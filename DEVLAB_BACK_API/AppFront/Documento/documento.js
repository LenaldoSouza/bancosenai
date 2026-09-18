const URL_API = "https://localhost:0000/api/v1/documento";

async function buscarDocumentos(codigoCliente = null) {

    if (!codigoCliente) {
        codigoCliente = document.getElementById("codigoCliente").value;
    }

    if (!codigoCliente) {
        alert("Informe o código do cliente.");
        return;
    }

    try {

        const response = await fetch(`${ URL_API } /listar/${ codigoCliente } `);

        if (!response.ok) {
            alert("Erro ao buscar documentos.");
            return;
        }

        const documentos = await response.json();

        const tabela = document.getElementById("tabelaDocumentos");

        tabela.innerHTML = "";

        documentos.forEach(documento => {

            const linha = document.createElement("tr");

            linha.innerHTML = `
    < td > ${ documento.id }</td >
                <td>${documento.nome}</td>
                <td>${documento.extensao}</td>

                <td>
                    <button
                        type="button"
                        onclick="baixarDocumento(${documento.id})">
                        Baixar
                    </button>

                    <button
                        type="button"
                        onclick="excluirDocumento(${documento.id}, ${codigoCliente})">
                        Excluir
                    </button>
                </td>
`;

            tabela.appendChild(linha);
        });

    } catch (erro) {

        console.error(erro);
        alert("Erro ao conectar com a API.");
    }
}


async function baixarDocumento(id) {

    try {

        const response = await fetch(`${ URL_API } /download/${ id } `);

        if (!response.ok) {
            alert("Erro ao baixar o documento.");
            return;
        }

        const blob = await response.blob();

        const url = window.URL.createObjectURL(blob);

        const link = document.createElement("a");

        link.href = url;

        link.download = `documento_${ id } `;

        document.body.appendChild(link);

        link.click();

        link.remove();

        window.URL.revokeObjectURL(url);

    } catch (erro) {

        console.error(erro);
        alert("Erro ao conectar com a API.");
    }
}


async function excluirDocumento(id, codigoCliente) {

    const confirmar = confirm(
        "Tem certeza que deseja excluir este documento?"
    );

    if (!confirmar) {
        return;
    }

    try {

        const response = await fetch(`${ URL_API } /delete/${ id } `, {
            method: "DELETE"
        });

        if (!response.ok) {
            alert("Erro ao excluir o documento.");
            return;
        }

        alert("Documento excluído com sucesso!");

        await buscarDocumentos(codigoCliente);

    } catch (erro) {

        console.error(erro);
        alert("Erro ao conectar com a API.");
    }
}


async function enviarDocumento() {

    const codigoCliente =
        document.getElementById("codigoCliente").value;

    const inputArquivo =
        document.getElementById("arquivo");

    const arquivo = inputArquivo.files[0];

    if (!codigoCliente || !arquivo) {

        alert(
            "Informe o código do cliente e selecione um arquivo"
        );

        return;
    }

    const dadosArquivo = new FormData();

    dadosArquivo.append("arquivo", arquivo);

    try {

        const response = await fetch(
            `${ URL_API } /upload/${ codigoCliente } `,
            {
                method: "POST",
                body: dadosArquivo
            }
        );

        if (!response.ok) {

            alert("Erro ao enviar o documento.");

            return;
        }

        alert("Documento enviado com sucesso!");

        // HU04
        // Busca novamente os documentos após o upload
        await buscarDocumentos(codigoCliente);

        document.getElementById("codigoCliente").value = "";
        document.getElementById("arquivo").value = "";

    } catch (erro) {

        console.error(erro);

        alert("Erro ao conectar com a API.");
    }
}