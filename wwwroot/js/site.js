const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))

function calcularValorTotal(p, q, r, s, size) {
    calcularValorParcial(p, q, r);
    atribuirTotal(s, size);

}

function calcularValorParcial(p, q, r) {
    var unidadeSemVirgula = (document.getElementById(p).innerText).toString();
    var valorUnidade = unidadeSemVirgula.replace(',', '.');
    valorUnidade = parseFloat(valorUnidade) || 0;

    var inputQtd = parseFloat(document.getElementById(q).value) || 0;


    var valorParcial = parseFloat(document.getElementById(r).innerText) || 0;

    valorParcial = (valorUnidade * inputQtd).toFixed(2);

    document.getElementById(r).innerText = valorParcial.toString();
};

function cancelarNota() {
    window.location.href = '@Url.Action("Index", "novoPedido")';
}

function atribuirTotal(s, size) {

    var valorTotal = parseFloat(valorTotal);
    valorTotal = 0;

    for (var i = 1; i <= size; i++) {
        i = i.toString();
        var valorParcial = document.getElementById(i + "-lista").innerText || 0;
        valorParcial = parseFloat(valorParcial);
        valorTotal += valorParcial;
    }
    valorTotal = valorTotal.toFixed(2)

    document.getElementById(s).innerText = valorTotal.toString().replace('.', ',');
}

// document.addEventListener("DOMContentLoaded", function (){

//         var data = new Date();

//         var ano = data.getFullYear();
//         var mes = (data.getMonth() + 1).toString().padStart(2, '0');
//         var dia = data.getDate().toString().padStart(2, '0');

//         var dataHoje = `${ano}-${mes}-${dia}`;
        
//         document.getElementById("Data_aluguel").value = dataHoje;
        
// });


// function SomarTotalDesc(i,j,k,l){
//     var inputSubTotal = document.getElementById(i).value || 0;
//     var inputDesc = document.getElementById(j).value || 0;
//     var inputPagPrevio = document.getElementById(l).value || 0;

//     var subTotal = parseFloat(inputSubTotal);
//     var desconto = parseFloat(inputDesc);
//     var previo = parseFloat(inputPagPrevio);

//     var total = subTotal-subTotal*(desconto/100);
    
//     if(total <0){
//         total = 0;
//     }
    
//     if(previo > total){
//         previo = total;
//         document.getElementById(l).value = previo.toFixed(2).replace('.',',');
//     }else

//     document.getElementById(k).value = total.toFixed(2).replace('.',',');
    

// }

function verificarFiltro(e){
    const item = document.getElementById(e).value;
    if(item.toLowerCase() === 'periodo'){
        document.getElementById('barraPesquisa').style.display = 'none';
        document.getElementById('barraPeriodo').style.display = 'flex';
    }else{
        document.getElementById('barraPesquisa').style.display = 'flex';
        document.getElementById('barraPeriodo').style.display = 'none';
    }
}

function verificarCadastro(){
    const nome = document.getElementById('nome').value;
    const senha = document.getElementById('senha').value;
    const confirmarSenha = document.getElementById('confirmarSenha').value;
    const email = document.getElementById('email').value;
    const confirmarEmail = document.getElementById('confirmarEmail').value;

    if(nome === '' || senha === '' || email === ''){
        alert("Por favor, preencha todos os campos!")
        return false;
    }else if(senha === confirmarSenha && email === confirmarEmail){
        return true;
    }else if(senha !== confirmarSenha){
        alert("As senhas não coincidem, por favor preencha os campos com a mesma senha")
        return false;
    }else if(email !== confirmarEmail){
        alert("Os e-mails não coincidem, por favor preencha os campos com o mesmo e-mail")
        return false;
    }
}

function test(){
    console.log('submitou')
}

function abrirMenuMobile(){
    
    const menu = document.getElementById('BurguerClicked');

    menu.style.display = 'block';
}
function fecharMenuMobile(){

    const menu = document.getElementById('BurguerClicked');

    menu.style.display = 'none';
}

function verificarCamposVazios() {
    var inputs = document.getElementsByClassName('form-control');

    var todosCamposVazios = Array.from(inputs).every(function(input) {
        return input.value.trim() === '';
    });

    if (todosCamposVazios) {
        alert('Nenhum campo foi preenchido!');
        return false;
    }

    return true;
}

function verificarNovosDados(){
    const nome = document.getElementById('nome').value;
    const novaSenha = document.getElementById('novaSenha').value;
    const confirmarNovaSenha = document.getElementById('confirmarNovaSenha').value;

    if(nome === ''){
        alert("Por favor, preencha o campo de nome!")
        return false;
    }else if(novaSenha == '' && confirmarNovaSenha == ''){
        return true;
    }else if(novaSenha !== confirmarNovaSenha){
        alert("As senhas não coincidem, por favor preencha os campos com a mesma senha")
        return false;
    }else{
        return true;
    }
}

document.addEventListener("DOMContentLoaded", function() {
    CalcularValorRestante();
});

function CalcularValorRestante(){
    var valorRestante = document.getElementById("pagRestante").value
    var valorPrevio = document.getElementById("pagPrevio").value || 0;
    var valorTotal = (document.getElementById("valor_total").value).toString();
    var valorEntrega = document.getElementById("taxaEntrega").value || 0;
    const qtdDias = calcularPeriodo('Data_recolhimento', 'Data_aluguel');
    
    const calcularTotal = () =>{
        if(qtdDias === 0){
            return total * 1
        }else{
            return total * qtdDias
        }
    }
    
    var total = valorTotal.replace(',', '.');

    var entrega = parseFloat(valorEntrega) || 0;

    var previo = valorPrevio.toString().replace(',','.');
    previo = parseFloat(previo);

    
    var somaTotal = entrega + ((total * qtdDias) - previo); 
    if(somaTotal <=0){
        somaTotal = 0;
    }


    document.getElementById("valor_total_visible").value = calcularTotal().toFixed(2).toString().replace('.',',');
    document.getElementById("pagRestante").value = somaTotal.toFixed(2).toString().replace('.',',');
    
}

function calcularPeriodo( data1 , data2 ){
    
    const dataFinal = document.getElementById(data1).value
    const dataInicio = document.getElementById(data2).value

    data1 = new Date(dataFinal)
    data2 = new Date(dataInicio)

    var diferenca = Math.abs(data1.getTime() - data2.getTime())

    var dias = diferenca / (1000*60*60*24);

    return dias;
    
    }

    function validarNumeros(item) {
        
        const input = document.getElementById(item);
        input.value = input.value.replace(/[^0-9]/g, '');
        var mensagemErro = document.getElementById("mensagemErro");
        if (input.value.match(/[^0-9]/g)) {
          mensagemErro.textContent = "Apenas números são permitidos.";
        } else {
          mensagemErro.textContent = "";
        }
      }

      function formatarCPF(item) {
        const input = document.getElementById(item)

        const cpf = input.value.replace(/\D/g, '');

        cpf = cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
        
        input.value = cpf;
      }