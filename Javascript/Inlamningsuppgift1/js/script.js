    'use strict';
    const cache = new Cache();
    const swPersonsURL = 'http://swapi.dev/api/people/?page=1';

    const swElements = {
        personTalbe: document.getElementById('sw-person-table'),
        planetTable: document.getElementById('sw-person-info-planet'),
        filmTable: document.getElementById('sw-person-info-films'),
        btnNext: document.getElementById('sw-button-next'),
        btnPrev: document.getElementById('sw-button-prev'),
        infoWindow: document.getElementById('sw-person-info')
    }

    const tableKeys = {
        [swElements.personTalbe.id] : [
            "name", "gender", "birth_year", "height", "mass", "hair_color", "skin_color", "eye_color"
        ],
        [swElements.filmTable.id] : [
            "title", "episode_id", "director", "producer", "release_date"
        ],
        [swElements.planetTable.id] : [
            "name", "rotation_period", "orbital_period", "diameter", "climate", "gravity", "terrain", "surface_water", "population"
        ]
    };

    const swClass = {
        selectedRow: 'sw-selected-row',
        row: 'sw-row'
    }

    const errorMessages = {
        invalidURL: 'Invalid URL.',
        invalidPerson: 'Person not found.'
    }

    const getJsonData = async (url, callback) => {
        try {
            const cachedData = cache.get(url);
            if(cachedData !== null){
                callback(cachedData);
            } else {
                const response = await fetch(url);
                const data = await response.json();
                callback(data);
                cache.add(url, data);
            }
        } catch (err) {
            console.error(err.message);
        }
    }

    function setPage(url) {
        if (url === null || url === undefined) {
            console.error(errorMessages.invalidURL);
            return;
        }
        swElements.infoWindow.hidden = true;
        swElements.btnPrev.disabled = true;
        swElements.btnNext.disabled = true;
        getJsonData(url, handlePersonData);
    }

    function highlightPersonRow(person){
        for (let child = swElements.personTalbe.firstChild; child !== null; child = child.nextSibling) {
            if (person.name !== undefined && child.id === person.name) {
                child.classList.add(swClass.selectedRow);
            } else {
                child.classList.remove(swClass.selectedRow);
            }
        }
    }

    function clickPerson(event) {
        event.preventDefault();

        const parentNode = event.srcElement.parentNode;

        // Check if this person is already selected
        if (parentNode.classList.contains(swClass.selectedRow)) {
            return;
        }

        const person = cache.get(parentNode.id);

        if(person === null){
            console.error(errorMessages.invalidPerson);
            return;
        }

        highlightPersonRow(person);

        // Set planet
        TableHelper.clearRows(swElements.planetTable);
        getJsonData(person.homeworld, handlePlanetData);

        // Set films
        TableHelper.clearRows(swElements.filmTable);

        person.films.forEach(film => {
            getJsonData(film, handleFilmData);
        });

        swElements.infoWindow.hidden = false;
    }

    const createTableHeader = element => {
        TableHelper.createHeader(element, tableKeys[element.id]);
    }

    //#region Handle data
    function handlePersonData(data) {
        TableHelper.clearRows(swElements.personTalbe);

        data.results.forEach(person => {
            let row = TableHelper.createRow(person, tableKeys[swElements.personTalbe.id]);
            row.id = person.name;
            row.className += swClass.row
            row.addEventListener(events.click, clickPerson);
            swElements.personTalbe.appendChild(row);
            if(cache.get(person.name) === null){
                cache.add(person.name, person);
            }
        });

        updatePageButton(swElements.btnPrev, data.previous);
        updatePageButton(swElements.btnNext, data.next);
    }

    const handleFilmData = data => handleTableRowData(data, swElements.filmTable);
    const handlePlanetData = data => handleTableRowData(data, swElements.planetTable);

    function handleTableRowData(data, table) {
        const row = TableHelper.createRow(data, tableKeys[table.id]);
        table.appendChild(row);
    }
    //#endregion

    //#region buttons
    function buttonClick(clickEvent) {
        setPage(clickEvent.srcElement.value)
    }

    function updatePageButton(button, url) {
        button.value = url;
        button.disabled = url === null;
    }
    //#endregion

    //#region input
    function handleKeyboardInput(keyDownEvent) {
        if(keyDownEvent.keyCode === KeyCodes.ARROW_RIGHT) {
            swElements.btnNext.click();
        } else if (keyDownEvent.keyCode === KeyCodes.ARROW_LEFT){
            swElements.btnPrev.click();
        }
    }
    //#endregion

    // Init tables
    createTableHeader(swElements.personTalbe);
    createTableHeader(swElements.planetTable);
    createTableHeader(swElements.filmTable);

    // Buttons
    swElements.btnPrev.addEventListener(events.click, buttonClick);
    swElements.btnNext.addEventListener(events.click, buttonClick);

    // Input
    document.addEventListener(events.keydown, handleKeyboardInput);

    // Fetch start page
    setPage(swPersonsURL);