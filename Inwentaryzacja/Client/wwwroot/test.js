export function dropDown(id_dropdown, id_button)
{
    var dropdown = document.getElementById(id_dropdown);
    /*var dropdownBtn = document.querySelector(".test");*/
    var dropdownBtn = document.getElementById(id_button);

    dropdownBtn.addEventListener("click", function ()
    {
        console.log("id_button: " + id_button);
        console.log("id_dropdown: " + id_dropdown);
        if (dropdown.style.display === "block")
        {
            dropdown.style.display = "none";
        }
        else
        {
            dropdown.style.display = "block";
        }
    }
    );
}

export function toUpperCase(element)
{
    element.value = element.value.toUpperCase();
}

export function WriteCookie(name, value, days)
{
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

export function ReadCookie(cname)
{
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++)
    {
        var c = ca[i];
        while (c.charAt(0) == ' ')
        {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0)
        {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
