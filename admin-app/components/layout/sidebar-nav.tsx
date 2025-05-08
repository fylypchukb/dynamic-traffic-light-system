"use client";

import Link from "next/link";
import {usePathname} from "next/navigation";
import {
    LayoutDashboard,
    Layers,
    MapPin,
    Settings,
    TrendingUp,
    LogOut
} from "lucide-react";

import {cn} from "@/lib/utils";
import {Button} from "@/components/ui/button";

interface NavItem {
    title: string;
    href: string;
    icon: React.ReactNode;
}

export function SidebarNav() {
    const pathname = usePathname();

    const navItems: NavItem[] = [
        {
            title: "Dashboard",
            href: "/dashboard",
            icon: <LayoutDashboard className="h-5 w-5"/>,
        },
        {
            title: "Intersections",
            href: "/intersections",
            icon: <MapPin className="h-5 w-5"/>,
        },
        {
            title: "Traffic Lights",
            href: "/traffic-lights",
            icon: <Layers className="h-5 w-5"/>,
        },
        {
            title: "Configurations",
            href: "/configurations",
            icon: <Settings className="h-5 w-5"/>,
        },
        {
            title: "Traffic Flow",
            href: "/traffic-flow",
            icon: <TrendingUp className="h-5 w-5"/>,
        },
    ];

    return (
        <div className="flex h-screen flex-col border-r">
            <div className="flex h-14 items-center border-b px-4">
                <Link
                    href="/dashboard"
                    className="flex items-center gap-2 font-semibold"
                >
                    <div
                        className="flex items-center justify-center w-8 h-8 rounded-full bg-primary text-primary-foreground">
                        <Layers className="h-5 w-5"/>
                    </div>
                    <span>Traffic Admin</span>
                </Link>
            </div>
            <div className="flex-1 overflow-auto py-2">
                <nav className="grid gap-1 px-2">
                    {navItems.map((item) => (
                        <Button
                            key={item.href}
                            variant={pathname.startsWith(item.href) ? "secondary" : "ghost"}
                            className={cn(
                                "justify-start gap-2",
                                pathname.startsWith(item.href) && "bg-secondary"
                            )}
                            asChild
                        >
                            <Link href={item.href}>
                                {item.icon}
                                {item.title}
                            </Link>
                        </Button>
                    ))}
                </nav>
            </div>
            <div className="mt-auto p-4 border-t">
                <Button variant="outline" className="w-full justify-start gap-2">
                    <LogOut className="h-4 w-4"/>
                    Logout
                </Button>
            </div>
        </div>
    );
}