// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
        [...document.querySelectorAll("img")].forEach(img => {
            if (!img.hasAttribute("src")) {
                console.log("img test");
                img.src = 'data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7';
            }
        })

const kill = document.getElementById("kill");
const critical = document.getElementById("critical")

function showHitIndicator() {
    const hit = document.getElementById("hit-indicator");

    hit.style.display = "block";
    if (kill) {
        kill.disabled = true;
    }
    if (critical) {
        critical.disabled = true;
    }

    setTimeout(function () {
        hit.style.display = "none";
        if (kill) {
            kill.disabled = false;
        }
        if (critical) {
            critical.disabled = false;
        }
    }, 500);
}

function showCritIndicator() {
    const crit = document.getElementById("crit-indicator");

    crit.style.display = "block";
    if (kill) {
        kill.disabled = true;
    }
    if (critical) {
        critical.disabled = true;
    }

    setTimeout(function () {
        crit.style.display = "none";
        if (kill) {
            kill.disabled = false;
        }
        if (critical) {
            critical.disabled = false;
        }
    }, 500);
}

function showFailIndicator() {
    const fail = document.getElementById("fail-indicator");

    fail.style.display = "block";
    if (kill) {
        kill.disabled = true;
    }
    if (critical) {
        critical.disabled = true;
    }

    setTimeout(function () {
        fail.style.display = "none";
        if (kill) {
            kill.disabled = false;
        }
        if (critical) {
            critical.disabled = false;
        }
    }, 500);
}

function showBlockIndicator() {
    const block = document.getElementById("block-indicator");

    block.style.display = "block";
    if (kill) {
        kill.disabled = true;
    }
    if (critical) {
        critical.disabled = true;
    }

    setTimeout(function () {
        block.style.display = "none";
        if (kill) {
            kill.disabled = false;
        }
        if (critical) {
            critical.disabled = false;
        }
    }, 500);
}