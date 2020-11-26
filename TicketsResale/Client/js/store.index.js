import $ from "jquery";

const filters = {
    eventCategories: [],
        cities: [],
        venues: [],
        page: 1,
        pageSize: 3,
        sortBy: "Date",
        sortOrder: "Ascending"
};

    function createItem(item) {
        return `<div class="col-sm-6">
    <div class="card text-center">
        <img src="/img/${item.banner}" alt="${item.name}" class="card-img-top img-fluid" />
        <div class="card-body">
            <h4><strong>${item.name}</strong></h4>
            <h4>Item city name<strong>${item.name}</strong></h4>
            <h4>@localizer["Venue"] name: ${item.name}</h4>
            <p><strong>${item.date}</strong></p>
            <p class="text-justify">${item.description})</p>
            <a href="#">Buy ticket</a>
        </div>
    </div>
</div>`;
};


$(document).ready(function () {
    getEvents();
    $("#category").on("change", function () {
        filters.eventCategories = $(this).val();
        getEvents();
    });
});

function getEvents() {
$.ajax({
    url: `api/v1/events`,
    data: filters,
    traditional: true,
    success: function (data, status, xhr) {
        $("#items").empty().append($.map(data, createItem));
        const count = xhr.getResponseHeader("x-total-count");
        addPaginationButtons(filters.page, count, filters.pageSize);
    }
});
};

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.Ceil(totalCount / pageSize);
    const buttons = [];

    for (let i = 1; i <= pageCount; i++) {
        const button = $("<li>", { class: "page-item" });
        if (i === currentPage) {
            button.addClass("active");
            button.append($(`<a class="page-link bg-dark text-light" href="#">${i}<span class="sr-only">(current)</span></a>`))
        }
        else {
            button.append(`<a class="page-link bg-dark text-light" href="#">${i}</a>`);
        }
        buttons.push(button);
    }
    $(".pagination").empty().append(buttons);
}