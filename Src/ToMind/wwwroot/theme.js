(function () {
    const key = "tomind-theme";
    const lightKey = "tomind-light-theme";
    const lightThemes = ["slate", "sand", "mist", "lavender", "mint", "sky", "blush", "olive"];
    const root = document.documentElement;

    function normalize(theme) {
        return theme === "dark" ? "dark" : "light";
    }

    function normalizeLight(theme) {
        return lightThemes.includes(theme) ? theme : "slate";
    }

    function apply(theme) {
        root.dataset.theme = theme;
    }

    function applyLight(theme) {
        root.dataset.light = theme;
    }

    function getStored() {
        return window.localStorage.getItem(key);
    }

    function setStored(theme) {
        window.localStorage.setItem(key, theme);
    }

    function getStoredLight() {
        return window.localStorage.getItem(lightKey);
    }

    function setStoredLight(theme) {
        window.localStorage.setItem(lightKey, theme);
    }

    window.ToMindTheme = {
        getTheme: function () {
            return normalize(getStored() || root.dataset.theme || "light");
        },
        getLightTheme: function () {
            return normalizeLight(getStoredLight() || root.dataset.light || "slate");
        },
        setTheme: function (theme) {
            const next = normalize(theme);
            apply(next);
            setStored(next);
            if (next === "light") {
                applyLight(normalizeLight(getStoredLight() || root.dataset.light || "slate"));
            }
            return next;
        },
        toggleTheme: function () {
            const current = normalize(getStored() || root.dataset.theme || "light");
            const next = current === "dark" ? "light" : "dark";
            apply(next);
            setStored(next);
            if (next === "light") {
                applyLight(normalizeLight(getStoredLight() || root.dataset.light || "slate"));
            }
            return next;
        },
        cycleLightTheme: function () {
            const current = normalizeLight(getStoredLight() || root.dataset.light || "slate");
            const index = lightThemes.indexOf(current);
            const next = lightThemes[(index + 1) % lightThemes.length];
            applyLight(next);
            setStoredLight(next);
            return next;
        },
        setLightTheme: function (theme) {
            const next = normalizeLight(theme);
            applyLight(next);
            setStoredLight(next);
            return next;
        },
        applyStoredTheme: function () {
            const stored = getStored();
            const theme = normalize(stored || "light");
            apply(theme);
            applyLight(normalizeLight(getStoredLight() || root.dataset.light || "slate"));
        }
    };

    const transparentGif = "data:image/gif;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=";
    let dragGhost = null;

    function moveGhost(event) {
        if (!dragGhost) {
            return;
        }
        dragGhost.style.left = event.clientX + "px";
        dragGhost.style.top = event.clientY + "px";
    }

    function removeGhost() {
        if (dragGhost) {
            dragGhost.remove();
            dragGhost = null;
        }
    }

    function applyDragPreview(event) {
        const target = event.target && event.target.closest
            ? event.target.closest(".tm-drag-item")
            : null;
        if (!target || !event.dataTransfer) {
            return;
        }

        event.dataTransfer.effectAllowed = "move";
        try {
            event.dataTransfer.setData("text/plain", "");
        } catch {
            // Ignore setData errors (some browsers restrict types).
        }

        const img = new Image();
        img.src = transparentGif;
        event.dataTransfer.setDragImage(img, 0, 0);

        removeGhost();
        dragGhost = target.cloneNode(true);
        dragGhost.classList.add("tm-drag-ghost");
        dragGhost.style.width = target.offsetWidth + "px";
        document.body.appendChild(dragGhost);
        moveGhost(event);
    }

    document.addEventListener("dragstart", applyDragPreview);
    document.addEventListener("drag", moveGhost);
    document.addEventListener("dragover", moveGhost);
    document.addEventListener("drop", removeGhost);
    document.addEventListener("dragend", removeGhost);
    window.ToMindTheme.applyStoredTheme();
})();
