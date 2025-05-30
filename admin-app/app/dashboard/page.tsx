"use client";

import React, {useEffect, useMemo, useState} from "react";
import {Bar, BarChart, CartesianGrid, Legend, ResponsiveContainer, Tooltip, XAxis, YAxis} from "recharts";
import {Card, CardContent, CardDescription, CardHeader, CardTitle} from "@/components/ui/card";
import {Configuration, EntityChangeLog, Intersection, TrafficLight, TrafficSwitchLog} from "@/lib/api-types";
import {configurationApi, intersectionApi, logsApi, trafficLightApi} from "@/lib/api";
import {Activity, AlertTriangle, CheckCircle, MapPin} from "lucide-react";
import {PageLoading} from "@/components/layout/page-loading";

export default function Dashboard() {
    const [isLoading, setIsLoading] = useState(true);
    const [configurations, setConfigurations] = useState<Configuration[]>([]);
    const [intersections, setIntersections] = useState<Intersection[]>([]);
    const [trafficLights, setTrafficLights] = useState<TrafficLight[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [trafficLogs, setTrafficLogs] = useState<TrafficSwitchLog[]>([]);
    const [entityLogs, setEntityLogs] = useState<EntityChangeLog[]>([]);

    const [fromDate, toDate] = [
        new Date(Date.now() - 1000 * 60 * 60 * 24).toISOString(), // last 24h
        new Date().toISOString(),
    ];

    useEffect(() => {
        async function fetchData() {
            try {
                setIsLoading(true);

                const [configsResponse, intersectionsResponse, lightsResponse, logsResp, entityLogResp] = await Promise.all([
                    configurationApi.getAll(),
                    intersectionApi.getAll(),
                    trafficLightApi.getAll(),
                    logsApi.getTrafficSwitchLogs({from: fromDate, to: toDate}),
                    logsApi.getEntityChangeLogs({}),
                ]);

                setConfigurations(configsResponse.result || []);
                setIntersections(intersectionsResponse.result || []);
                setTrafficLights(lightsResponse.result || []);
                setTrafficLogs(logsResp.result || []);
                setEntityLogs(entityLogResp.result || []);
            } catch (err) {
                console.error("Error fetching dashboard data:", err);
                setError("Failed to load dashboard data. Please try again later.");
            } finally {
                setIsLoading(false);
            }
        }

        fetchData();
    }, []);

    // Demo chart data
    const trafficData = useMemo(() => {
        const buckets: Record<string, { cars: number; duration: number }> = {};

        trafficLogs.forEach((log) => {
            const hour = new Date(log.timestamp).getHours();
            let label;
            if (hour >= 6 && hour < 12) label = "Morning";
            else if (hour >= 12 && hour < 17) label = "Afternoon";
            else if (hour >= 17 && hour < 21) label = "Evening";
            else label = "Night";

            if (!buckets[label]) buckets[label] = {cars: 0, duration: 0};

            buckets[label].cars += log.vehicleCount;
            buckets[label].duration += log.greenLightDurationSeconds;
        });

        return Object.entries(buckets).map(([name, {cars, duration}]) => ({
            name,
            cars,
            duration,
        }));
    }, [trafficLogs]);

    const sortedEntityLogs = useMemo(() =>
        [...entityLogs].sort((a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime()), [entityLogs]);

    if (isLoading) {
        return <PageLoading/>;
    }

    if (error) {
        return (
            <div className="flex justify-center items-center h-96">
                <div className="text-center">
                    <AlertTriangle className="h-12 w-12 text-red-500 mx-auto mb-4"/>
                    <h2 className="text-xl font-bold">Error Loading Dashboard</h2>
                    <p className="text-muted-foreground mt-2">{error}</p>
                </div>
            </div>
        );
    }

    const activeIntersections = intersections.filter(i => i.isActive).length;
    const activeTrafficLights = trafficLights.filter(t => t.isActive).length;
    const activeConfigs = configurations.filter(c => c.isActive).length;

    return (
        <div className="space-y-6">
            <h1 className="text-3xl font-bold tracking-tight">Dashboard</h1>

            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-sm font-medium">Intersections</CardTitle>
                        <MapPin className="h-4 w-4 text-muted-foreground"/>
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold">{intersections.length}</div>
                        <p className="text-xs text-muted-foreground flex items-center gap-1 mt-1">
                            <CheckCircle className="h-3 w-3 text-green-500"/>
                            {activeIntersections} active
                        </p>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-sm font-medium">Traffic Lights</CardTitle>
                        <div className="flex space-x-1">
                            <div className="h-3 w-3 rounded-full bg-red-500"/>
                            <div className="h-3 w-3 rounded-full bg-amber-500"/>
                            <div className="h-3 w-3 rounded-full bg-green-500"/>
                        </div>
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold">{trafficLights.length}</div>
                        <p className="text-xs text-muted-foreground flex items-center gap-1 mt-1">
                            <CheckCircle className="h-3 w-3 text-green-500"/>
                            {activeTrafficLights} active
                        </p>
                    </CardContent>
                </Card>

                <Card>
                    <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                        <CardTitle className="text-sm font-medium">Configurations</CardTitle>
                        <Activity className="h-4 w-4 text-muted-foreground"/>
                    </CardHeader>
                    <CardContent>
                        <div className="text-2xl font-bold">{configurations.length}</div>
                        <p className="text-xs text-muted-foreground flex items-center gap-1 mt-1">
                            <CheckCircle className="h-3 w-3 text-green-500"/>
                            {activeConfigs} active
                        </p>
                    </CardContent>
                </Card>
            </div>

            <div className="grid gap-6 md:grid-cols-2">
                <Card>
                    <CardHeader>
                        <CardTitle>Traffic Analysis</CardTitle>
                        <CardDescription>Traffic volume and green light duration by time of day</CardDescription>
                    </CardHeader>
                    <CardContent>
                        <ResponsiveContainer width="100%" height={350}>
                            <BarChart data={trafficData}>
                                <CartesianGrid strokeDasharray="3 3"/>
                                <XAxis dataKey="name"/>
                                <YAxis yAxisId="left" orientation="left" stroke="hsl(var(--chart-1))"/>
                                <YAxis yAxisId="right" orientation="right" stroke="hsl(var(--chart-2))"/>
                                <Tooltip/>
                                <Legend/>
                                <Bar yAxisId="left" dataKey="cars" name="Vehicle Count" fill="hsl(var(--chart-1))"/>
                                <Bar yAxisId="right" dataKey="duration" name="Green Light Duration (s)"
                                     fill="hsl(var(--chart-2))"/>
                            </BarChart>
                        </ResponsiveContainer>
                    </CardContent>
                </Card>

                <div className="grid gap-6">
                    <Card>
                        <CardHeader>
                            <CardTitle>Recent Activity</CardTitle>
                            <CardDescription>Latest system changes and events</CardDescription>
                        </CardHeader>
                        <CardContent>
                            <div className="space-y-4">
                                {sortedEntityLogs.slice(0, 5).map((log) => (
                                    <div key={log.id}
                                         className="flex items-start gap-4 border-b pb-4 last:border-0 last:pb-0">
                                        <div className="rounded-full p-2 bg-muted">
                                            <Activity className="h-4 w-4"/>
                                        </div>
                                        <div>
                                            <p className="font-medium">
                                                {log.action} {log.entityName} ID #{log.entityId}
                                            </p>
                                            <p className="text-sm text-muted-foreground">
                                                {new Date(log.timestamp).toLocaleString()}
                                            </p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </CardContent>
                    </Card>
                </div>
            </div>
        </div>
    );
}