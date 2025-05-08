"use client";

import React, { useEffect, useState } from "react";
import { ColumnDef } from "@tanstack/react-table";
import { DataTable } from "@/components/ui/data-table";
import { PanelHeader } from "@/components/ui/panel-header";
import { intersectionApi, trafficLightApi } from "@/lib/api";
import { Intersection, TrafficLight } from "@/lib/api-types";
import { Edit, Layers, MoreHorizontal, Trash } from "lucide-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { StatusBadge } from "@/components/ui/status-badge";
import { PageLoading } from "@/components/layout/page-loading";
import { EmptyState } from "@/components/ui/empty-state";
import { formatDistanceToNow } from "date-fns";
import Link from "next/link";

export default function TrafficLightsPage() {
  const [trafficLights, setTrafficLights] = useState<TrafficLight[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function fetchData() {
      try {
        setIsLoading(true);
        const response = await trafficLightApi.getAll();

        setTrafficLights(response.result || []);
      } catch (err) {
        console.error("Failed to fetch traffic lights:", err);
        setError("Failed to load traffic lights. Please try again.");
      } finally {
        setIsLoading(false);
      }
    }

    fetchData();
  }, []);

  const columns: ColumnDef<TrafficLight>[] = [
    {
      accessorKey: "id",
      header: "ID",
    },
    {
      accessorKey: "name",
      header: "Name",
    },
    {
      accessorKey: "intersectionName",
      header: "Intersection",
    },
    {
      accessorKey: "priority",
      header: "Priority",
    },
    {
      accessorKey: "isActive",
      header: "Status",
      cell: ({ row }) => {
        return <StatusBadge active={row.original.isActive} />;
      },
    },
    {
      accessorKey: "lastUpdateTime",
      header: "Last Updated",
      cell: ({ row }) => {
        return formatDistanceToNow(new Date(row.original.lastUpdateTime), { addSuffix: true });
      },
    },
    {
      id: "actions",
      cell: ({ row }) => {
        return (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Open menu</span>
                <MoreHorizontal className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem asChild>
                <Link href={`/traffic-lights/${row.original.id}`}>
                  <Edit className="mr-2 h-4 w-4" />
                  Edit
                </Link>
              </DropdownMenuItem>
              <DropdownMenuItem className="text-destructive">
                <Trash className="mr-2 h-4 w-4" />
                Delete
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
    },
  ];

  if (isLoading) {
    return <PageLoading />;
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

  if (trafficLights.length === 0) {
    return (
      <div className="space-y-6">
        <PanelHeader
          title="Traffic Lights"
          description="Manage traffic lights in your intersections"
          createHref="/traffic-lights/new"
        />
        <EmptyState
          title="No traffic lights found"
          description="Create your first traffic light to get started"
          icon={<Layers className="h-10 w-10 text-muted-foreground" />}
          actionHref="/traffic-lights/new"
          actionText="Create Traffic Light"
        />
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <PanelHeader
        title="Traffic Lights"
        description="Manage traffic lights in your intersections"
        createHref="/traffic-lights/new"
      />
      <DataTable
        columns={columns}
        data={trafficLights}
        searchColumn="name"
        searchPlaceholder="Search traffic lights..."
      />
    </div>
  );
}