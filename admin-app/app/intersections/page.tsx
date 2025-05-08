"use client";

import React, {useEffect, useRef, useState} from "react";
import {ColumnDef} from "@tanstack/react-table";
import {DataTable} from "@/components/ui/data-table";
import {PanelHeader} from "@/components/ui/panel-header";
import {intersectionApi} from "@/lib/api";
import {Intersection} from "@/lib/api-types";
import {Edit, MoreHorizontal, Trash} from "lucide-react";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import {Button} from "@/components/ui/button";
import {StatusBadge} from "@/components/ui/status-badge";
import {PageLoading} from "@/components/layout/page-loading";
import {EmptyState} from "@/components/ui/empty-state";
import {formatDistanceToNow} from "date-fns";
import {MapPin} from "lucide-react";
import Link from "next/link";

export default function IntersectionsPage() {
    const [intersections, setIntersections] = useState<Intersection[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {

        async function fetchIntersections() {
            try {
                setIsLoading(true);
                const response = await intersectionApi.getAll();
                setIntersections(response.result || []);
            } catch (err) {
                console.error("Failed to fetch intersections:", err);
                setError("Failed to load intersections. Please try again.");
            } finally {
                setIsLoading(false);
            }
        }

        fetchIntersections();
    }, []);

    const columns: ColumnDef<Intersection>[] = [
        {
            accessorKey: "id",
            header: "ID",
        },
        {
            accessorKey: "city",
            header: "City",
        },
        {
            accessorKey: "location",
            header: "Location",
        },
        {
            accessorKey: "isActive",
            header: "Status",
            cell: ({row}) => {
                return <StatusBadge active={row.original.isActive}/>;
            },
        },
        {
            accessorKey: "lastUpdateTime",
            header: "Last Updated",
            cell: ({row}) => {
                const utcDate = new Date(row.original.lastUpdateTime + 'Z');
                return formatDistanceToNow(utcDate, {addSuffix: true});
            },
        },
        {
            id: "actions",
            cell: ({row}) => {
                return (
                    <DropdownMenu>
                        <DropdownMenuTrigger asChild>
                            <Button variant="ghost" className="h-8 w-8 p-0">
                                <span className="sr-only">Open menu</span>
                                <MoreHorizontal className="h-4 w-4"/>
                            </Button>
                        </DropdownMenuTrigger>
                        <DropdownMenuContent align="end">
                            <DropdownMenuItem asChild>
                                <Link href={`/intersections/${row.original.id}`}>
                                    <Edit className="mr-2 h-4 w-4"/>
                                    Edit
                                </Link>
                            </DropdownMenuItem>
                            <DropdownMenuItem className="text-destructive">
                                <Trash className="mr-2 h-4 w-4"/>
                                Delete
                            </DropdownMenuItem>
                        </DropdownMenuContent>
                    </DropdownMenu>
                );
            },
        },
    ];

    if (isLoading) {
        return <PageLoading/>;
    }

    if (error) {
        return (
            <div className="flex justify-center items-center h-96">
                <div className="text-center">
                    <p className="text-red-500">{error}</p>
                    <Button onClick={() => window.location.reload()} className="mt-4">
                        Try Again
                    </Button>
                </div>
            </div>
        );
    }

    if (intersections.length === 0) {
        return (
            <div className="space-y-6">
                <PanelHeader
                    title="Intersections"
                    description="Manage traffic intersections across the city"
                    createHref="/intersections/new"
                />
                <EmptyState
                    title="No intersections found"
                    description="Create your first intersection to get started"
                    icon={<MapPin className="h-10 w-10 text-muted-foreground"/>}
                    actionHref="/intersections/new"
                    actionText="Create Intersection"
                />
            </div>
        );
    }

    return (
        <div className="space-y-6">
            <PanelHeader
                title="Intersections"
                description="Manage traffic intersections across the city"
                createHref="/intersections/new"
            />
            <DataTable
                columns={columns}
                data={intersections}
                searchColumn="location"
                searchPlaceholder="Search locations..."
            />
        </div>
    );
}