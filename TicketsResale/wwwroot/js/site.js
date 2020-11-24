// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function getEvents(eventCategoryId) {
    const response = await fetch(`/api/v1/eventcategories/${eventCategoryId}/events`, {
        method: "GET",
        headers: { "Accept": "Application/json" }
    })

    if (response.ok === true) {
        const events = await response.json();
        //debugger;
        const newBody = document.createElement("tbody");
        events.forEach(e => {
            const tr = document.createElement("tr");
            
            const tdBanner = document.createElement("td");
            tdBanner.append(e.banner);
            tr.append(tdBanner);

            const tdName = document.createElement("td");
            tdName.append(e.name);
            tr.append(tdName);
            /*
            const tdCity = document.createElement("td");
            tdCity.append(e.venue.city.name);
            tr.append(tdCity);

            const tdVenue = document.createElement("td");
            tdVenue.append(e.venue.name);
            tr.append(tdVenue);*/

            const tdDate = document.createElement("td");
            tdDate.append(e.date);
            tr.append(tdDate);

            const tdDescription = document.createElement("td");
            tdDescription.append(e.description);
            tr.append(tdDescription);

            newBody.append(tr);
        })
        const oldBody = document.querySelector("tbody");
        
        oldBody.parentNode.replaceChild(newBody, oldBody);
        debugger;

    }
}