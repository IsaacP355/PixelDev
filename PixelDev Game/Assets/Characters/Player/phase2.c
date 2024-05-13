
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <X11/Xlib.h>
#include <X11/keysym.h>
#include <X11/keysymdef.h>
#include <unistd.h>
#include <sys/types.h>
#include <time.h>
#include <sys/ipc.h>//for IPC message queue
#include <sys/msg.h>//^^
#include <stdlib.h> // for rand() and srand()
#include <time.h>   // for time()

unsigned long colors[5]; // Array to store colors

struct Global {
    Display *dpy;
    Window win;
    GC gc;
    int xres, yres;
    int color;
} g;

#define MSG_SIZE 256

struct Message {
    long mtype;
    char mtext[MSG_SIZE];
};


void x11_cleanup_xwindows(void);
void x11_init_xwindows(void);
void x11_clear_window(void);
void check_mouse(XEvent *e);
int check_keys(XEvent *e);
void render(void);
void render_parent(void); //only for parent
void generate_random_colors();


int myargc;
char **myargv;
char **myenvp;
pid_t cpid = 0;
int usagePrinted = 0; //flag for usage statement

int main(int argc, char *argv[], char *envp[]) {

    myargc = argc;
    myargv = argv;
    myenvp = envp;
    generate_random_colors();

    XEvent e;
    int done = 0;
    x11_init_xwindows();
    int countdown = 0;
    g.color = 1;
    XClearWindow(g.dpy, g.win);


    // Parent process
    int msgid; // Message Queue ID

    // Create a Message Queue
    msgid = msgget(IPC_PRIVATE, IPC_CREAT | 0666);
    if (msgid == -1) {
        perror("msgget");
        exit(EXIT_FAILURE);
    }

    // Send message to the child
    struct Message msg;
    msg.mtype = 1; // Message type (can be any positive value)
    strcpy(msg.mtext, "Message from parent to child");
    if (msgsnd(msgid, &msg, sizeof(msg.mtext), 0) == -1) {
        perror("msgsnd");
        exit(EXIT_FAILURE);
    }

    if(argc == 2) {  
        countdown = atoi(argv[1]);
    } else if (argc < 2 && !usagePrinted) { //i added usagePrinted so that usage is only once
        printf("Usage: %s n\n", argv[0]);
        usagePrinted = 1;
    }

    while(!done) {
        /* Check the event queue */
        while (XPending(g.dpy)) {
            XNextEvent(g.dpy, &e);
            check_mouse(&e);
            done = check_keys(&e);
            render();
        }
        if ( countdown > 0 ) {
            if (--countdown == 0)
                done = 1;
        }
        usleep(4000);
    }
    x11_cleanup_xwindows();
    return 0;
}
void x11_cleanup_xwindows(void) {
    XDestroyWindow(g.dpy, g.win);
    XCloseDisplay(g.dpy);
}
void x11_init_xwindows(void) {
    int scr;

    if (!(g.dpy = XOpenDisplay(NULL))) {
        fprintf(stderr, "ERROR: could not open display!\n");
        exit(EXIT_FAILURE);
    }
    scr = DefaultScreen(g.dpy);
    g.xres = 400;
    g.yres = 200;
    g.win = XCreateSimpleWindow(g.dpy, RootWindow(g.dpy, scr), 1, 1,
            g.xres, g.yres, 0, 0x00ffffff, 0x00000000);
    XStoreName(g.dpy, g.win, "cs3600 xwin sample");
    g.gc = XCreateGC(g.dpy, g.win, 0, NULL);
    XMapWindow(g.dpy, g.win);
    XSelectInput(g.dpy, g.win, ExposureMask | StructureNotifyMask |
            PointerMotionMask | ButtonPressMask |
            ButtonReleaseMask | KeyPressMask);
}
/*void check_mouse(XEvent *e) {
    static int savex = 0;
    static int savey = 0;
    int mx = e->xbutton.x;
    int my = e->xbutton.y;

    if (e->type != ButtonPress
            && e->type != ButtonRelease
            && e->type != MotionNotify)
        return;
    if (e->type == ButtonPress) {
        if (e->xbutton.button==1) { }
        if (e->xbutton.button==3) { }
    }
    if (e->type == MotionNotify) {
        if (savex != mx || savey != my) {
            //mouse moved
            savex = mx;
            savey = my;

            if(cpid == 1) {
                fflush(stdout);
                static int count2 = 0;
                count2++;
                if(count2 % 10 == 0)
                    printf("c");
            } else {
                fflush(stdout);
                static int count = 0;
                count++;
                if(count % 10 == 0)
                    printf("m");

            }
        }
    }
}*/
void check_mouse(XEvent *e) {
    static int savex = 0;
    static int savey = 0;
    int mx = e->xbutton.x;
    int my = e->xbutton.y;

    if (e->type != ButtonPress && e->type != ButtonRelease && e->type != MotionNotify)
        return;

    if (e->type == ButtonPress) {
        if (e->xbutton.button == 1) {
            // Check if the click is inside any of the colored boxes in the parent window
            int boxSize = 50;
            for (int i = 0; i < 5; ++i) {
                if (mx >= 50 + i * (boxSize + 10) && mx <= 50 + i * (boxSize + 10) + boxSize &&
                    my >= 80 && my <= 80 + boxSize) {
                    // Clicked on a colored box in the parent window, change child's background color
                    if (cpid == 1) {
                        // Change child window's background color
                        XSetWindowBackground(g.dpy, g.win, colors[i]);
                        XClearWindow(g.dpy, g.win);
                        XFlush(g.dpy); // Ensure changes are applied immediately
                    }
                    return;
                }
            }
            // Check if the click is inside the black box in the parent window
            if (mx >= 50 + 5 * (boxSize + 10) && mx <= 50 + 5 * (boxSize + 10) + boxSize &&
                my >= 80 && my <= 80 + boxSize) {
                // Clicked on the black box in the parent window, close the child window
                if (cpid == 1) {
                    x11_cleanup_xwindows();
                    exit(0); // or whatever termination mechanism you prefer
                }
                return;
            }
        }
    }

    if (e->type == MotionNotify) {
        if (savex != mx || savey != my) {
            /* mouse moved */
            savex = mx;
            savey = my;

            if (cpid == 1) {
                fflush(stdout);
                static int count2 = 0;
                count2++;
                if (count2 % 10 == 0)
                    printf("c");
            } else {
                fflush(stdout);
                static int count = 0;
                count++;
                if (count % 10 == 0)
                    printf("m");
            }
        }
    }
}


int check_keys(XEvent *e) {
    int key;
    static int one = 1;
    if (e->type != KeyPress && e->type != KeyRelease)
        return 0;
    key = XLookupKeysym(&e->xkey, 0);
    if (e->type == KeyPress) {
        switch (key) {
            case XK_c:
                cpid = fork();
                if (cpid == 0) {
                    cpid = 1;
                    main(1, myargv, myenvp);
                    exit(0);
                }
                break;
            case XK_Escape:
                return 1;
            case XK_a:
                fflush(stdout);
                one *= -1;
                g.color *= -1;
        }
    }
    return 0;
}
/*/========================================================================
  void x11_setFont(unsigned int idx)
  {
  char *fonts[] = { "fixed","5x8","6x9","6x10","6x12","6x13","6x13bold",
  "7x13","7x13bold","7x14","8x13","8x13bold","8x16","9x15","9x15bold",
  "10x20","12x24" };
  Font f = XLoadFont(g.dpy, fonts[idx]);
  XSetFont(g.dpy, g.gc, f);
  }
  */
void render(void) {

    if (cpid == 1) {
        XClearWindow(g.dpy, g.win);//what is this for again?
        XSetForeground(g.dpy, g.gc, 0x007393B3);
        XFillRectangle(g.dpy, g.win, g.gc, 0, 0, g.xres, g.yres);
        //White text
        XSetForeground(g.dpy, g.gc, 0xFFFFFF); 
        //The string in a char
        //font
        XSetFont(g.dpy, g.gc, XLoadFont(g.dpy, "9x15bold"));
        // First line
        char *str1 = "Child window";
        XDrawString(g.dpy, g.win, g.gc, 10, 20, str1, strlen(str1)); // Drawing first line of text

        // Second line
        char *str2 = "Press Esc to close";
        XDrawString(g.dpy, g.win, g.gc, 10, 40, str2, strlen(str2));
    } else {
        XSetForeground(g.dpy, g.gc, 0x00F5B041);
        XFillRectangle(g.dpy, g.win, g.gc, 0, 0, g.xres, g.yres);
        //White text
        XSetForeground(g.dpy, g.gc, 0x353535); 
        //The string in a char
        XSetFont(g.dpy, g.gc, XLoadFont(g.dpy, "9x15bold"));

        // First line
        char *str1 = "Parent window";
        XDrawString(g.dpy, g.win, g.gc, 10, 20, str1, strlen(str1));

        // Second line
        char *str2 = "Press 'C' for child window";
        XDrawString(g.dpy, g.win, g.gc, 10, 40, str2, strlen(str2));
        render_parent(); // Call the new render_parent
    }
}


void generate_random_colors() {
    // Seed the random number generator
    srand(time(NULL));

    // Generate random colors for the first four boxes
    for (int i = 0; i < 4; ++i) {
        colors[i] = rand() % 0xFFFFFF; // Generate a random color
    }
    // The fifth box is always black
    colors[4] = 0x000000;
}

void render_parent(void) {

    // Draw little colored boxes
    int boxSize = 50;

    // Draw the boxes
    for (int i = 0; i < 5; ++i) {
        XSetForeground(g.dpy, g.gc, colors[i]);
        XFillRectangle(g.dpy, g.win, g.gc, 50 + i * (boxSize + 10), 80, boxSize, boxSize);
    }
}