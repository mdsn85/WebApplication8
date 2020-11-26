(function (factory) {
  if (typeof define === 'function' && define.amd) {
    // AMD. Register as an anonymous module depending on jQuery.
    define(['jquery'], factory);
  } else {
    // No AMD. Register plugin with global jQuery object.
    factory(jQuery);
  }
}(function ($) {
    $.fn.initKeyboard = function(o) {
        // Scope
        var s = {
            el: $(this), // Element 
            list: [], // List of active keys
            debug: (typeof o !== 'undefined' && typeof o.debug !== 'undefined' && o.debug), // Debug
            // Translated Keys
            map: {
                'Dead': '',
                'Meta': 'Cmd',
                'Control': 'Ctrl',
                'Arrowup': '▲',
                'Arrowdown': '▼',
                'Arrowleft': '◀',
                'Arrowright': '▶'
            },

            // Return key combo
            combo: function() {
                return this.list.sort().sort(function(a,b){
                    return b.length - a.length;
                }).join('+');
            },

            // Translate keys
            translate: function(k) {
                if (typeof this.map[k] !== 'undefined') {
                    k = this.map[k];
                }

                return k;
            },

            // Format & translate the input key
            format: function(k) {
                k = k.replace(/\w\S*/g, function(k){
                    return k.charAt(0).toUpperCase() + k.substr(1).toLowerCase();
                });

                return this.translate(k).trim();
            },

            // Add key to list
            add: function(k) {
                // Add Key
                if (s.list.indexOf(k) < 0) {
                    s.list.push(k);
                }
            },

            // Remove key from list
            remove: function(k) {
                var i = this.list.indexOf(k);
                if (i >= 0) {
                    this.list.splice(i, 1);
                }
            },

            // Trigger key combination
            trigger: function() {
                this.el.trigger( this.combo() );
                if (this.debug) {
                    console.log( this.combo() );
                }
            },

            // Reset combo
            reset: function() {
                this.list = [];
            }
        };

        // Bind Key Events
        s.el.on('keydown', function(e){
            e = s.format(e.key);
            if (e !== '') {
                s.add(e);
                s.trigger();
            }
        }).on('keyup', function(e){
            e = s.format(e.key);
            s.remove(e);
        }).on('blur focusout', function(){
            s.reset();
        });

        return s.el;
    };
}));