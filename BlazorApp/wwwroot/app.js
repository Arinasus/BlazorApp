window.showDeleteModal = () => {
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
        const modal = bootstrap.Modal.getOrCreateInstance(modalElement);
        modal.show();
    }
};

window.hideDeleteModal = () => {
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
        const modal = bootstrap.Modal.getInstance(modalElement);
        if (modal) {
            modal.hide();
        }
    }
};

document.addEventListener('hidden.bs.modal', function () {
    document.querySelectorAll('.modal-backdrop').forEach(b => b.remove());
    document.body.classList.remove('modal-open');
    document.body.style.overflow = '';
    document.body.style.paddingRight = '';
});

window.showCategoryModal = () => {
    const modal = bootstrap.Modal.getOrCreateInstance(document.getElementById('categoryModal'));
    modal.show();
};

window.hideCategoryModal = () => {
    const modalElement = document.getElementById('categoryModal');
    const modal = bootstrap.Modal.getInstance(modalElement);
    if (modal) {
        modal.hide();
    }
};