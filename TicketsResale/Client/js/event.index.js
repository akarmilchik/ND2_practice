import $ from 'jquery';
import 'bootstrap';
import 'bootstrap-select';
import 'bootstrap-autocomplete';

const filtersEvent = {
    eventCategories: [],
    cities: [],
    venues: [],
    page: 1,
    pageSize: 4,
    sortBy: "Date",
    sortOrder: "Ascending",
    dateFrom: "",
    dateTo: "",
    searchString: ""
};

const filtersVenue = {
    cities: []
}

function createEventItem(item) {
    return `<div class="col-sm-6">
            <div class="card text-center text-white bg-dark mb-3">
                <img src="/img/${item.banner}" alt="${item.name}" class="card-img-top img-fluid" />
                <div class="card-body" ">
                    <h4><strong>${item.name}</strong></h4>
                    <p><strong>${item.cityName}</strong></p>
                    <p><strong>${item.venueName}</strong></p>
                    <p><strong>${item.date}</strong></p>
                    <p class="text-justify">${item.description})</p>
                    <a href="${options.eventTicketsUrl + "?eventId=" + item.id}" class="btn btn-primary pt-1 mb-1">Buy ticket</a>
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

    $("#cat2").on("change", function () {
        filtersEvent.eventCategories = $(this).val();
        getEvents();
    });

    $("#category").on("change", function () {
        filtersEvent.eventCategories = $(this).val();
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

    $("#search").on("click", function () {
        filtersEvent.searchString = $("#autosuggest").val();
        getEvents();
    });

    $('#autosuggest').autoComplete({
        resolverSettings: {
            url: 'api/v1/events/autosuggest',
        },
    });

    // $("#autosuggest").autoComplete({
    //     resolver: "custom",
    //     events: {
    //         search: function (searchString, response) {
    //             $.ajax({
    //                 url: "api/v1/events/autosuggest",
    //                 data: { filtersEvent, searchString: searchString },
    //                 traditional: true,
    //                 success: function (data, xhr) {
    //                     const suggestedData = data.value.map(item => {
    //                         return { value: item.name, text: item.name };
    //                     });
    //                     response(suggestedData);
    //                 }
    //             });
    //         }
    //     }
    // });
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
        const count = xhr.getResponseHeader('x-total-count');
        addPaginationButtons(filtersEvent.page, count, filtersEvent.pageSize);
    }
});
};

function getVenues() {
    $.ajax({
        url: "api/v1/venues",
        data: filtersVenue,
        traditional: true,
        success: function (data, status) {
            $("#venue").empty().append($.map(data, createVenueItem));
            $("#venue").selectpicker("refresh");
        }
    });
}

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.ceil(totalCount / pageSize);
    const buttons = [];

    for (let i = 1; i <= pageCount; i++) {
        const button = $("<li>", { class: "page-item" });
        if (i === currentPage) {
            button.addClass("active");
            button.append($(`<a class="page-link" href="#">${i}<span class="sr-only">(current)</span></a>`))
        }
        else {
            button.append(`<a class="page-link bg-dark text-light" href="#">${i}</a>`);
        }
        button.data("page", i);
        buttons.push(button);
    }
    $(".pagination").empty().append(buttons);
    $(".pagination").addClass("justify-content-center");
    $(".page-item").on("click", function () {
        filtersEvent.page = $(this).data("page");
        getEvents();
    });
}

function addInitialDate(num) {
    if (num < 10)
    {
        num = '0' + num;
    }
    return num;
}



