var simplemaps_countrymap_mapdata = {
    main_settings: {
        //General settings
        width: "460", //or 'responsive'
        background_color: "#FFFFFF",
        background_transparent: "yes",
        border_color: "#ffffff",
        pop_ups: "detect",

        //State defaults
        state_description: "State description",
        state_color: "#1C155E",
        state_hover_color: "#8DEF8F",
        state_url: "",
        border_size: 1.5,
        all_states_inactive: "no",
        all_states_zoomable: "no",

        //Location defaults
        location_description: "Location description",
        location_url: "",
        location_color: "#ff0000",
        location_opacity: 0.8,
        location_hover_opacity: 1,
        location_size: 10,
        location_type: "square",
        location_image_source: "frog.png",
        location_border_color: "#FFFFFF",
        location_border: 1,
        location_hover_border: 2.5,
        all_locations_inactive: "no",
        all_locations_hidden: "no",


        //Label defaults
        label_color: "#d5ddec",
        label_hover_color: "#d5ddec",
        label_size: 22,
        label_font: "Arial",
        hide_labels: "no",
        hide_eastern_labels: "no",

        //Zoom settings
        zoom: "no",
        manual_zoom: "no",
        back_image: "no",
        initial_back: "no",
        initial_zoom: "-1",
        initial_zoom_solo: "no",
        region_opacity: 1,
        region_hover_opacity: 0.6,
        zoom_out_incrementally: "yes",
        zoom_percentage: 0.99,
        zoom_time: 0.5,

        //Popup settings
        popup_color: "white",
        popup_opacity: 0.9,
        popup_shadow: 1,
        popup_corners: 5,
        popup_font: "12px/1.5 Verdana, Arial, Helvetica, sans-serif",
        popup_nocss: "no",

        //Advanced settings
        div: "mapregion",
        auto_load: "yes",
        url_new_tab: "no",
        images_directory: "default",
        fade_time: 0.1,
        link_text: "View Website"


    },
    state_specific: {
        LKA2448: {
            name: "KANDY",
            description: "default",
            color: "default",
            hover_color: "default",
            url: "javascript:clickedFunction('KY')"
        },
        LKA2449: {
            name: "MATALE",
            description: "Matale District",
            url: "javascript:clickedFunction('MT')"
        },
        LKA2450: {
            name: "NUWARA ELIYA",
            description: "Nuwara Eliya District",
            url: "javascript:clickedFunction('NW')"
        },
        LKA2451: {
            name: "AMPARA",
            description: "Ampara District",
            url: "javascript:clickedFunction('APR')"
        },
        LKA2452: {
            name: "BATTICALOA",
            description: "Batticaloa District",
            url: "javascript:clickedFunction('BC')"
        },
        LKA2453: {
            name: "POLONNARUWA",
            description: "Polonnaruwa District",
            url: "javascript:clickedFunction('PR')"
        },
        LKA2454: {
            name: "TRINCOMALEE",
            description: "Trincomalee District",
            url: "javascript:clickedFunction('TC')"
        },
        LKA2455: {
            name: "ANURADHAPURA",
            description: "Anuradhapura District",
            url: "javascript:clickedFunction('AD')"
        },
        LKA2456: {
            name: "VAVUNIYA",
            description: "Vavuniya District",
            url: "javascript:clickedFunction('VA')"
        },
        LKA2457: {
            name: "MANNAR",
            description: "Manna District",
            url: "javascript:clickedFunction('MB')"
        },
        LKA2458: {
            name: "MULLAITIVU",
            description: "Mulaitiv District",
            url: "javascript:clickedFunction('MP')"
        },
        LKA2459: {
            name: "JAFFNA",
            description: "Jaffna District",
            url: "javascript:clickedFunction('JA')"
        },
        LKA2460: {
            name: "KILINOCHCHI",
            description: "Kilinochchi District",
            url: "javascript:clickedFunction('KIL')"
        },
        LKA2461: {
            name: "KURUNEGALA",
            description: "Kuruṇegala District",
            url: "javascript:clickedFunction('KG')"
        },
        LKA2462: {
            name: "PUTTALAM",
            description: "Puttalam District",
            url: "javascript:clickedFunction('PX')"
        },
        LKA2463: {
            name: "RATNAPURA",
            description: "Ratnapura District",
            url: "javascript:clickedFunction('RN')"
        },
        LKA2464: {
            name: "GALLE",
            description: "Galle District",
            url: "javascript:clickedFunction('GL')"
        },
        LKA2465: {
            name: "HAMBANTOTA",
            description: "Hambantoṭa District",
            url: "javascript:clickedFunction('HB')"
        },
        LKA2466: {
            name: "MATARA",
            description: "Matara District",
            url: "javascript:clickedFunction('MH')"
        },
        LKA2467: {
            name: "BADULLA",
            description: "Badulla District",
            url: "javascript:clickedFunction('BD')"
        },
        LKA2468: {
            name: "MONERAGALA",
            description: "Moṇeragala District",
            url: "javascript:clickedFunction('MJ')"
        },
        LKA2469: {
            name: "KEGALLE",
            description: "Kegalle District",
            url: "javascript:clickedFunction('KE')"
        },
        LKA2470: {
            name: "COLOMBO",
            description: "Colombo District",
            url: "javascript:clickedFunction('CO')"
        },
        LKA2471: {
            name: "GAMPAHA",
            description: "Gampaha District",
            url: "javascript:clickedFunction('GQ')"
        },
        LKA2472: {
            name: "KALUTARA",
            description: "Kalutara District",
            url: "javascript:clickedFunction('KT')"
        }
    }
        ,
    locations: {
        "0": {
            lat: "6.9187973",
            lng: "79.8502431",
            name: "Colombo",
            description: "Head Office",
            size: "25",
            color: "#ff0067"
        },
        "1": {
            lat: "6.9456133",
            lng: "79.8479188",
            name: "Colombo",
            description: "Port of Colombo",
            color: "#0a47fc",
            size: "20"
        },
        "2": {
            lat: "6.1180074",
            lng: "81.1022709",
            name: "Hambanthota",
            description: "Magam Ruhunupura Mahinda Rajapaksa Port",
            color: "#0a47fc",
            size: "20"
        },
        "3": {
            lat: "7.1801499",
            lng: "79.8664824",
            name: "Katunayake",
            description: "Bandaranaike International Airport",
            color: "#07f61d",
            size: "20"
        },
        "4": {
            lat: "6.2919149",
            lng: "81.0508647",
            name: "Mattala",
            description: "Mattala Rajapaksa International Airport",
            color: "#07f61d",
            size: "20",
            image_url: "http://localhost:15514/Images/favicon.png"
        }
    },
    regions: {
        "0": {
            states: [
              "LKA2448",
              "LKA2449",
              "LKA2450"
            ],
            name: "CENTRAL PROVICE",
            //color: "RED",
            //hover_color: "WHITE",
            description: "CENTRAL PROVICE",
            zoomable: "no",
            url: "javascript:clickedFunction('CENTRAL','REGION')"
        },
        "1": {
            states: [
              "LKA2451",
              "LKA2452",
              "LKA2454"
            ],
            name: "EASTERN PROVINCE",
            //color: "#1456c9",
            //hover_color: "WHITE",
            description: "EASTERN PROVINCE",
            zoomable: "no",
            url: "javascript:clickedFunction('EASTERN','REGION')"
        },
        "2": {
            states: [
              "LKA2455",
              "LKA2453"
            ],
            name: "NORTH CENTRAL",
            //color: "#60c914",
           // hover_color: "WHITE",
            description: "NORTH CENTRAL",
            zoomable: "no",
            url: "javascript:clickedFunction('NORTH CENTRAL','REGION')"
        },
        "3": {
            states: [
              "LKA2459",
              "LKA2460",
              "LKA2457",
              "LKA2458",
              "LKA2456"
            ],
            name: "NORTH PROVINCE",
            //color: "#e2a7ca",
            //hover_color: "WHITE",
            description: "NORTH PROVINCE",
            zoomable: "no",
            url: "javascript:clickedFunction('NORTHERN','REGION')"
        },
        "4": {
            states: [
              "LKA2461",
              "LKA2462"
            ],
            name: "NORTH WESTERN",
            //color: "#f3d365",
            //hover_color: "WHITE",
            description: "NORTH WESTERN",
            zoomable: "no",
            url: "javascript:clickedFunction('NORTH WESTERN','REGION')"
        },
        "5": {
            states: [
              "LKA2469",
              "LKA2463"
            ],
            name: "SABARAGAMUWA PROVINCE",
            //color: "#a8a1a3",
            //hover_color: "WHITE",
            description: "SABARAGAMUWA PROVINCE",
            zoomable: "no",
            url: "javascript:clickedFunction('SABARAGAMUWA','REGION')"
        },
        "6": {
            states: [
              "LKA2464",
              "LKA2465",
              "LKA2466"
            ],
            name: "SOUTH PROVINCE",
            //color: "#9efeee",
           // hover_color: "WHITE",
            description: "SOUTH PROVINCE",
            zoomable: "no",
            url: "javascript:clickedFunction('SOUTHERN','REGION')"
        },
        "7": {
            states: [
              "LKA2470",
              "LKA2471",
              "LKA2472"
            ],
            name: "WESTERN PROVINCE",
            //color: "#503351",
            //hover_color: "WHITE",
            description: "WESTERN PROVINCE",
            zoomable: "no",
            url: "javascript:clickedFunction('WESTERN','REGION')"
        },
        "8": {
            states: [
              "LKA2467",
              "LKA2468"
            ],
            name: "UVA PROVINCE",
            //color: "#d8fa87",
            //hover_color: "WHITE",
            description: "UVA PROVINCE",
            zoomable: "no",
            url: "javascript:clickedFunction('UVA','REGION')"
        }
    }
}
