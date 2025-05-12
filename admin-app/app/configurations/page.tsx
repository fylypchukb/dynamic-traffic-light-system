"use client";

import React, {useEffect, useState} from "react";
import {ColumnDef} from "@tanstack/react-table";
import {DataTable} from "@/components/ui/data-table";
import {PanelHeader} from "@/components/ui/panel-header";
import {configurationApi} from "@/lib/api";
import {Configuration} from "@/lib/api-types";
import {Edit, MoreHorizontal, Settings, Trash} from "lucide-react";
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
import Link from "next/link";

export default function ConfigurationsPage() {
    const [configurations, setConfigurations] = useState<Configuration[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const fetchData = async () => {
        try {
            setIsLoading(true);
            const response = await configurationApi.getAll();

            setConfigurations(response.result || []);
        } catch (err) {
            console.error("Failed to fetch configurations:", err);
            setError("Failed to load configurations. Please try again.");
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    const columns: ColumnDef<Configuration>[] = [
        {
            accessorKey: "id",
            header: "ID",
        },
        {
            accessorKey: "trafficLightName",
            header: "Traffic Light"
        },
        {
            accessorKey: "minGreenTime",
            header: "Min Green (s)",
        },
        {
            accessorKey: "maxGreenTime",
            header: "Max Green (s)",
        },
        {
            accessorKey: "defaultGreenTime",
            header: "Default Green (s)",
        },
        {
            accessorKey: "defaultRedTime",
            header: "Default Red (s)",
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
                                <Link href={`/configurations/${row.original.id}`}>
                                    <Edit className="mr-2 h-4 w-4"/>
                                    Edit
                                </Link>
                            </DropdownMenuItem>
                            <DropdownMenuItem
                                className="text-destructive"
                                onClick={async () => {
                                    try {
                                        await configurationApi.delete(row.original.id);
                                        await fetchData();
                                    } catch (error) {
                                        console.error("Delete failed", error);
                                    }
                                }}>
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

    if (configurations.length === 0) {
        return (
            <div className="space-y-6">
                <PanelHeader
                    title="Configurations"
                    description="Manage traffic light configurations and timing settings"
                    createHref="/configurations/new"
                />
                <EmptyState
                    title="No configurations found"
                    description="Create your first configuration to get started"
                    icon={<Settings className="h-10 w-10 text-muted-foreground"/>}
                    actionHref="/configurations/new"
                    actionText="Create Configuration"
                />
            </div>
        );
    }

    return (
        <div className="space-y-6">
            <PanelHeader
                title="Configurations"
                description="Manage traffic light configurations and timing settings"
                createHref="/configurations/new"
            />
            <DataTable
                columns={columns}
                data={configurations}
            />
        </div>
    );
}