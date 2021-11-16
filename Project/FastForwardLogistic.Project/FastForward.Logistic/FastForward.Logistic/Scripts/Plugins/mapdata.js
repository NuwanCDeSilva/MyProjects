jQuery(document).ready(function () {

});
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
            state_color: "#F29302",
            state_hover_color: "#EAC996",
            state_url: "",
            border_size: 1.5,
            all_states_inactive: "no",
            all_states_zoomable: "no",

            //Location defaults
            location_description: "Location description",
            location_url: "",
            location_color: "#0021ff",
            location_icon: "",
            location_opacity: 0.8,
            location_hover_opacity: 1,
            location_size: 10,
            location_type: "circle",
            location_image_source: "frog",
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
            zoom: "yes",
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
            div: "map",
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
                url: "javascript:clickedFunction('KANDY','DISTRICT')"
            },
            LKA2449: {
                name: "MATALE",
                description: "Matale District",
                url: "javascript:clickedFunction('MATALE','DISTRICT')"
            },
            LKA2450: {
                name: "NUWARA ELIYA",
                description: "Nuwara Eliya District",
                url: "javascript:clickedFunction('NUWARA ELIYA','DISTRICT')"
            },
            LKA2451: {
                name: "AMPARA",
                description: "Ampara District",
                url: "javascript:clickedFunction('AMPARA','DISTRICT')"
            },
            LKA2452: {
                name: "BATTICALOA",
                description: "Batticaloa District",
                url: "javascript:clickedFunction('BATTICALOA','DISTRICT')"
            },
            LKA2453: {
                name: "POLONNARUWA",
                description: "Polonnaruwa District",
                url: "javascript:clickedFunction('POLONNARUWA','DISTRICT')"
            },
            LKA2454: {
                name: "TRINCOMALEE",
                description: "Trincomalee District",
                url: "javascript:clickedFunction('TRINCOMALEE','DISTRICT')"
            },
            LKA2455: {
                name: "ANURADHAPURA",
                description: "Anuradhapura District",
                url: "javascript:clickedFunction('ANURADHAPURA','DISTRICT')"
            },
            LKA2456: {
                name: "VAVUNIYA",
                description: "Vavuniya District",
                url: "javascript:clickedFunction('VAVUNIYA','DISTRICT')"
            },
            LKA2457: {
                name: "MANNAR",
                description: "Manna District",
                url: "javascript:clickedFunction('MANNAR','DISTRICT')"
            },
            LKA2458: {
                name: "MULLAITIVU",
                description: "Mulaitiv District",
                url: "javascript:clickedFunction('MULLAITIVU','DISTRICT')"
            },
            LKA2459: {
                name: "JAFFNA",
                description: "Jaffna District",
                url: "javascript:clickedFunction('JAFFNA','DISTRICT')"
            },
            LKA2460: {
                name: "KILINOCHCHI",
                description: "Kilinochchi District",
                url: "javascript:clickedFunction('KILINOCHCHI','DISTRICT')"
            },
            LKA2461: {
                name: "KURUNEGALA",
                description: "Kuruṇegala District",
                url: "javascript:clickedFunction('KURUNEGALA','DISTRICT')"
            },
            LKA2462: {
                name: "PUTTALAM",
                description: "Puttalam District",
                url: "javascript:clickedFunction('PUTTALAM','DISTRICT')"
            },
            LKA2463: {
                name: "RATNAPURA",
                description: "Ratnapura District",
                url: "javascript:clickedFunction('RATNAPURA','DISTRICT')"
            },
            LKA2464: {
                name: "GALLE",
                description: "Galle District",
                url: "javascript:clickedFunction('GALLE','DISTRICT')"
            },
            LKA2465: {
                name: "HAMBANTOTA",
                description: "Hambantoṭa District",
                url: "javascript:clickedFunction('HAMBANTOTA','DISTRICT')"
            },
            LKA2466: {
                name: "MATARA",
                description: "Matara District",
                url: "javascript:clickedFunction('MATARA','DISTRICT')"
            },
            LKA2467: {
                name: "BADULLA",
                description: "Badulla District",
                url: "javascript:clickedFunction('BADULLA','DISTRICT')"
            },
            LKA2468: {
                name: "MONERAGALA",
                description: "Moṇeragala District",
                url: "javascript:clickedFunction('MONERAGALA','DISTRICT')"
            },
            LKA2469: {
                name: "KEGALLE",
                description: "Kegalle District",
                url: "javascript:clickedFunction('KEGALLE','DISTRICT')"
            },
            LKA2470: {
                name: "COLOMBO",
                description: "Colombo District",
                url: "javascript:clickedFunction('COLOMBO','DISTRICT')"
            },
            LKA2471: {
                name: "GAMPAHA",
                description: "Gampaha District",
                url: "javascript:clickedFunction('GAMPAHA','DISTRICT')"
            },
            LKA2472: {
                name: "KALUTARA",
                description: "Kalutara District",
                url: "javascript:clickedFunction('KALUTARA','DISTRICT')"
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
        }
    }
    
