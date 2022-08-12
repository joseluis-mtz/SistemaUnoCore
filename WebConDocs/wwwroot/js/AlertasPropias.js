function MostrarModal(Titulo = "¿Estás seguro?", Texto = "Está acción guardara la especialidad") {
    return Swal.fire({
        title: Titulo,
        text: Texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, Confirmar'
    })
}