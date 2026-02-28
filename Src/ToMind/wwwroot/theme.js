(function () {
    const key = "tomind-theme";
    const root = document.documentElement;

    function normalize(theme) {
        return theme === "dark" ? "dark" : "light";
    }

    function apply(theme) {
        root.dataset.theme = theme;
    }

    function getStored() {
        return window.localStorage.getItem(key);
    }

    function setStored(theme) {
        window.localStorage.setItem(key, theme);
    }

    window.ToMindTheme = {
        getTheme: function () {
            return normalize(getStored() || root.dataset.theme || "light");
        },
        setTheme: function (theme) {
            const next = normalize(theme);
            apply(next);
            setStored(next);
            return next;
        },
        toggleTheme: function () {
            const current = normalize(getStored() || root.dataset.theme || "light");
            const next = current === "dark" ? "light" : "dark";
            apply(next);
            setStored(next);
            return next;
        },
        applyStoredTheme: function () {
            const stored = getStored();
            apply(normalize(stored || "light"));
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
