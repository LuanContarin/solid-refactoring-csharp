function addButtonEventRemoveAuction() {
    let removeAuctionButtons = document.querySelectorAll('.btnRemoveLeilao');
    removeAuctionButtons.forEach(btn => $(btn).on('click', () => {
        let auction = $(btn).data();
        if (window.confirm(`Confirm deletion of auction ${auction.titulo}?`)) {
            jQuery.ajax({
                url: `/Auction/Remove/${auction.id}`,
                method: 'post',
                success: () => $(`.row-leilao-${auction.id}`).hide('slow'),
                error: () => window.alert('An error ocurred when deleting')
            });
        }
    }));
}

$(document).ready(function () {
    addButtonEventRemoveAuction();

});