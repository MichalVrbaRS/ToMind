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

    window.ToMindTheme.applyStoredTheme();
})();
