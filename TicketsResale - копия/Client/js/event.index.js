import $ from "jquery";

const filtersEvent = {
    eventCategories: [],
    cities: [],
    venues: [],
    page: 1,
    pageSize: 3,
    sortBy: "Date",
    sortOrder: "Ascending",
    dateFrom: "",
    dateTo: "",
    searchText: ""
};

const filtersVenue = {
    cities: []
}

function createEventItem(item) {
    return `<div class="col-sm-6">
            <div class="card text-center">
                <img src="/img/${item.banner}" alt="${item.name}" class="card-img-top img-fluid" />
                <div class="card-body">
                    <h4><strong>${item.name}</strong></h4>
                    <h4>@localizer["City"]: <strong>${item.cityName}</strong></h4>
                    <h4>@localizer["Venue"]: ${item.venueName}</h4>
                    <p><strong>${item.date}</strong></p>
                    <p class="text-justify">${item.description})</p>
                    <a href="${props.showEventUrl + "?eventId=" + event.id}" class="btn btn-primary pt-1 mb-1">Buy ticket</a>
                    ${props.userInAdminRole === "true" ? "<a href=\"" + props.removeEventUrl + "?eventId=" + event.id + "\" class=\"btn btn-danger pt-1 mb-1\">Remove event</a>" : ""}
                </div>
            </div>
            </div>`;
};

function createVenueItem(item) {
    return `<option value="${item.id}">${item.name}</option>`
};

$(document).ready(function () {
    getEvents();
    getVenues();

    $("#category").on("change", function () {
        filters.eventCategories = $(this).val();
        getEvents();
    });

    $("#venue").on("change", function () {
        filtersEvent.venues = $(this).val();
        getEvents();
    });

    $("#city").on("change", function () {
        filtersVenue.cities = $(this).val();
        filtersEvent.cities = $(this).val();
        filtersEvent.venues = [];
        getVenues();
        getEvents();
    });


    $("#sortBy").on("change", function () {
        filtersEvent.sortBy = $(this).val();
        getEvents();
    });

    $("#sortOrder").on("change", function () {
        filtersEvent.sortOrder = $(this).val();
        getEvents();
    });

    $("#dateFrom").on("change", function () {
        filtersEvent.dateFrom = $(this).val();
        getEvents();
    });

    $("#dateTo").on("change", function () {
        filtersEvent.dateTo = $(this).val();
        getEvents();
    });

    $("#pageSize").on("change", function () {
        filtersEvent.pageSize = $(this).val();
        getEvents();
    });
});

function getEvents() {
$.ajax({
    url: `api/v1/events`,
    data: filtersEvent,
    traditional: true,
    success: function (data, status, xhr) {
        data = data.map(item => {
            const date = new Date(item.date);
            item.date = `${addInitialDate(date.getDate())}.${addInitialDate(date.getMonth() + 1)}.${date.getFullYear()}`;
            return item;
        });

        $("#eventItems").empty().append($.map(data, createEventItem));
        const count = xhr.getResponseHeader("x-total-count");
        addPaginationButtons(filters.page, count, filters.pageSize);
    }
});
};

function getVenues() {
    $.ajax({
        url: "/api/v1/venues",
        data: filtersVenue,
        traditional: true,
        success: function (data, status) {
            $("#venueItems").empty().append($.map(data, createVenueItem));
            $("#venueItems").selectpicker("refresh");
        }
    });
}

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

    $(".page-item").on("click", function () {
        filtersEvent.page = $(this).data("page");
        getEvents();
    });
}